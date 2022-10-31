using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtEffectScript : MonoBehaviour
{
    [Header("Efekt gaœnicy")]
    [SerializeField] GameObject extEffect;

    private bool isExtInUse;

    private void Start()
    {
        isExtInUse = false;
    }

    private void OnEnable()
    {
        EventManager.usingFEStart += ExtInUseSet;
        EventManager.EndOfusingFE += ExtInUseSet;
        EventManager.EndOfFiller += EndOfFiller;
    }

    private void OnDisable()
    {
        EventManager.usingFEStart -= ExtInUseSet;
        EventManager.EndOfusingFE -= ExtInUseSet;
        EventManager.EndOfFiller -= EndOfFiller;
    }

    private void ExtInUseSet()
    {
        isExtInUse = !isExtInUse;
    }

    private void EndOfFiller()
    {
        Destroy(extEffect);
    }

    private void Update()
    {
        extEffect.SetActive(isExtInUse);
    }

}
