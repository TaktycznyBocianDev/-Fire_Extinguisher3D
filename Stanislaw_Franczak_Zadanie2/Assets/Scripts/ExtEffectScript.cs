using UnityEngine;

public class ExtEffectScript : MonoBehaviour
{
    [Header("Efekt gaœnicy")]
    [SerializeField] GameObject fireExtinguisherEffect;

    private bool isFireExtinguisherInUse;

    private void Start()
    {
        isFireExtinguisherInUse = false;
    }

    private void OnEnable()
    {
        EventManager.StartUsingFireExtinguisher += FireExtinguisherInUseChange;
        EventManager.StopUsingFireExtinguisher += FireExtinguisherInUseChange;
        EventManager.FillerHasEnded += FillerHasEnded;
    }

    private void OnDisable()
    {
        EventManager.StartUsingFireExtinguisher -= FireExtinguisherInUseChange;
        EventManager.StopUsingFireExtinguisher -= FireExtinguisherInUseChange;
        EventManager.FillerHasEnded -= FillerHasEnded;
    }

    private void FireExtinguisherInUseChange()
    {
        isFireExtinguisherInUse = !isFireExtinguisherInUse;
    }

    private void FillerHasEnded()
    {
        fireExtinguisherEffect.SetActive(false);
    }

    private void Update()
    {
        //Uruchom efekt je¿eli gaœnica jest u¿ywana
        fireExtinguisherEffect.SetActive(isFireExtinguisherInUse);
    }

}
