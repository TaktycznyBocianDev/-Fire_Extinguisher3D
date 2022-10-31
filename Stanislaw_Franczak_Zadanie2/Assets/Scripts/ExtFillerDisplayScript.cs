using UnityEngine;
using UnityEngine.UI;

public class ExtFillerDisplayScript : MonoBehaviour
{
    [Header("Miejsce gdzie poziom wype³nienia gaœnicy bêdzie siê wyœwietlaæ.")]
    [SerializeField] Text text;

    [Header("Na jak¹ iloœæ czasu ma wystarczyæ gaœnica?")]
    [SerializeField] float fillerState;
    public float GetFillerState() { return fillerState; }

    private bool isExtUsed;

    private void Start()
    {
        text.text = fillerState.ToString();
        isExtUsed = false;
    }

    private void OnEnable()
    {
        EventManager.usingFEStart += UseEXSet;
        EventManager.EndOfusingFE += UseEXSet;
    }

    private void OnDisable()
    {
        EventManager.usingFEStart -= UseEXSet;
        EventManager.EndOfusingFE -= UseEXSet;
    }

    private void UseEXSet()
    {
        isExtUsed = !isExtUsed;
    }

    private void Update()
    {
        if (isExtUsed)
        {
            fillerState -= Time.deltaTime;
            text.text = fillerState.ToString("F2");
            WhenFillerEnds();
        }

    }

    private void WhenFillerEnds()
    {
        if (fillerState <= 0)
        {
            text.text = "0";
            EventManager.EndOfFillerFunc();
            this.GetComponent<Image>().color = Color.red;
        }
    }
}
