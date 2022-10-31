using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PutOutFire : MonoBehaviour
{
    [Header("Efekt ognia")]
    [SerializeField] ParticleSystem fireEffect;

    [Header("Nasycenie ognia")]
    [SerializeField] float powerOfFire;

    [Header("Iloœæ paliwa w gaœnicy")]
    [SerializeField] GameObject fireExtFiller;

    [Header("Gaœnica")]
    [SerializeField] GameObject fireExtinguisher;

    private float startingPowerOfFire;
    private float fireExtPower;
    private float fillerInExt;
    private bool isExInUse;

    private void Start()
    {
        startingPowerOfFire = powerOfFire;
        fillerInExt = fireExtFiller.GetComponent<ExtFillerDisplayScript>().GetFillerState();
        isExInUse = false;
    }
    private void OnEnable()
    {
        EventManager.usingFEStart += isExInUseSet;
        EventManager.EndOfusingFE += isExInUseSet;
        EventManager.EndOfFiller += TheEnd;
    }

    private void OnDisable()
    {
        EventManager.usingFEStart -= isExInUseSet;
        EventManager.EndOfusingFE -= isExInUseSet;
        EventManager.EndOfFiller -= TheEnd;
    }

    private void isExInUseSet()
    {
        isExInUse = !isExInUse;
    }

    private void TheEnd()
    {
        isExInUse = false;
    }

    private void Update()
    {
       
        if (isExInUse)
        {
            CalculatefireExtPower();
            powerOfFire -= Time.deltaTime * fireExtPower;
            var emision = fireEffect.emission;
            emision.rateOverTime = powerOfFire;
            if (emision.rateOverTime.constant <= 0.1f)
            {
                emision.rateOverTime = 0;
                powerOfFire = 0;
                Destroy(fireEffect);
            }

        }
        if (!isExInUse)
        {
            var emision = fireEffect.emission;
            if (emision.rateOverTime.constant <= startingPowerOfFire)
            {
                powerOfFire += Time.deltaTime * fireExtPower;
                emision.rateOverTime = powerOfFire;
                if (emision.rateOverTime.constant >= startingPowerOfFire)
                {
                    emision.rateOverTime = startingPowerOfFire;
                    powerOfFire = startingPowerOfFire;
                }
            }
        }
    }

    private void CalculatefireExtPower()
    {
        float perfectExPower = startingPowerOfFire / fillerInExt; //Dziel¹c moc ognia przez iloœæ œrodka gaœniczego dowiemy siê, jaka jest idealna "moc gaszenia".
        float perfectY = transform.position.y - 1; //Idealne ustawienie gaœnicy to na dole p³omienia. Jest to ustawienie na równi z boxem i dodatkowo - troche ni¿ej.
        float acctualY = fireExtinguisher.transform.position.y; //Takie ustawienie na osi y (jedynej modyfikowalnej) ma nasza gaœnica w rzeczywistoœci.
        float acctualPower = 0;

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
        
        



        fireExtPower =  acctualPower;
    }

}
