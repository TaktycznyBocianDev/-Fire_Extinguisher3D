using UnityEngine;

public class PutOutFire : MonoBehaviour
{
    [Header("Efekt ognia")]
    [SerializeField] ParticleSystem fireParticleEffect;

    [Header("Nasycenie ognia")]
    [SerializeField] float powerOfFire;

    [Header("Iloœæ paliwa w gaœnicy")]
    [SerializeField] GameObject fireExtinguisherFillerLevel;

    [Header("Gaœnica")]
    [SerializeField] GameObject fireExtinguisher;

    private float startingPowerOfFire;
    private float fillerInFireExtinguisher;
    private bool isFireExtinguisherInUse;
    private void isFireExtinguisherInUseChange()
    {
        isFireExtinguisherInUse = !isFireExtinguisherInUse;
    }
    private void TheEnd()
    {
        isFireExtinguisherInUse = false;
    }


    private void Start()
    {
        startingPowerOfFire = powerOfFire;
        fillerInFireExtinguisher = fireExtinguisherFillerLevel.GetComponent<ExtFillerDisplayScript>().GetFillerState();
        isFireExtinguisherInUse = false;
    }
    private void OnEnable()
    {
        EventManager.StartUsingFireExtinguisher += isFireExtinguisherInUseChange;
        EventManager.StopUsingFireExtinguisher += isFireExtinguisherInUseChange;
        EventManager.FillerHasEnded += TheEnd;
    }
    private void OnDisable()
    {
        EventManager.StartUsingFireExtinguisher -= isFireExtinguisherInUseChange;
        EventManager.StopUsingFireExtinguisher -= isFireExtinguisherInUseChange;
        EventManager.FillerHasEnded -= TheEnd;
    }

    private void Update()
    {
       
        if (isFireExtinguisherInUse)
        {
            FireExtinguisherIsCurrentlyInUse();
            TheEndOfFiller();
        }
        if (!isFireExtinguisherInUse)
        {
            FireExtinguisherIsNOTInUse();
        }

        ClampFirePower(0, startingPowerOfFire);
        
    }
    //Te dwie funkce odpowiadaj¹ za spadek/wzrost mocy p³omienia
    private void FireExtinguisherIsCurrentlyInUse()
    {
        
        powerOfFire -= Time.deltaTime * CalculatefireExtPower(); ;
        ParticleSystem.EmissionModule emision = fireParticleEffect.emission;
        emision.rateOverTime = powerOfFire;
        
    }
    private void FireExtinguisherIsNOTInUse()
    {
        ParticleSystem.EmissionModule emision = fireParticleEffect.emission;
        if (powerOfFire <= startingPowerOfFire && powerOfFire >= 0.15f)
        {
            powerOfFire += Time.deltaTime;
            emision.rateOverTime = powerOfFire;
            
        }
    }
    //Funkcja dba by moc ognia i emisja nie by³y zbyt du¿e/ma³e
    private void ClampFirePower(float minPowerOFFire, float maxPowerOfFire)
    {
        ParticleSystem.EmissionModule emision = fireParticleEffect.emission;
        if (emision.rateOverTime.constant >= maxPowerOfFire)
        {
            emision.rateOverTime = maxPowerOfFire;
            powerOfFire = maxPowerOfFire;
        }
        if (emision.rateOverTime.constant < minPowerOFFire)
        {
            emision.rateOverTime = minPowerOFFire;
            powerOfFire = minPowerOFFire;
        }
    }
    private void TheEndOfFiller()
    {
        ParticleSystem.EmissionModule emision = fireParticleEffect.emission;
        if (emision.rateOverTime.constant <= 0.15f)
        {
            emision.rateOverTime = 0;
            powerOfFire = 0;
            fireParticleEffect.gameObject.SetActive(false);
        }
    }
    private float CalculatefireExtPower()
    {
        float perfectExPower = startingPowerOfFire / fillerInFireExtinguisher; //Dziel¹c moc ognia przez iloœæ œrodka gaœniczego dowiemy siê, jaka jest idealna "moc gaszenia".
        float perfectY = transform.position.y - 1; //Idealne ustawienie gaœnicy to na dole p³omienia. Jest to ustawienie na równi z boxem i dodatkowo - troche ni¿ej.
        float acctualY = fireExtinguisher.transform.position.y; //Takie ustawienie na osi y (jedynej modyfikowalnej) ma nasza gaœnica w rzeczywistoœci.
        float acctualPower = 0;

        //Dystans od ognia zmiejsza stopniowo moc gaszenia
        if (Mathf.Abs(perfectY - acctualY) <= 3)
        {
            acctualPower = 0.6f * perfectExPower;
        }
        if (Mathf.Abs(perfectY - acctualY) <= 1.4)
        {
            acctualPower = 0.8f * perfectExPower;
        }
        if (Mathf.Abs(perfectY - acctualY) <= 0.4)
        {
            acctualPower = perfectExPower;
        }
        ;
        return  acctualPower;
    }

}
