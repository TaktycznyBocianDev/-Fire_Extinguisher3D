using UnityEngine;
using UnityEngine.UI;

public class ExtFillerDisplayScript : MonoBehaviour
{
    [Header("Na jak¹ iloœæ czasu ma wystarczyæ gaœnica?")]
    [SerializeField] float fillerState;
    [Header("Miejsce gdzie poziom wype³nienia gaœnicy bêdzie siê wyœwietlaæ.")]
    [SerializeField] Text text;

    //Poni¿szy prosty getter bêdzie potrzebny do skryptu gaszenia ognia
    public float GetFillerState() { return fillerState; }
    private bool isFireExtinguisherInUse;

    private void Start()
    {
        text.text = fillerState.ToString();
        isFireExtinguisherInUse = false;
    }

    private void OnEnable()
    {
        EventManager.StartUsingFireExtinguisher += UseFireExtinguisherSet;
        EventManager.StopUsingFireExtinguisher += UseFireExtinguisherSet;
    }

    private void OnDisable()
    {
        EventManager.StartUsingFireExtinguisher -= UseFireExtinguisherSet;
        EventManager.StopUsingFireExtinguisher -= UseFireExtinguisherSet;
    }

    private void UseFireExtinguisherSet()
    {
        isFireExtinguisherInUse = !isFireExtinguisherInUse;
    }

    private void Update()
    {
        if (isFireExtinguisherInUse)
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
            EventManager.FilerEndsFunc();
            this.GetComponent<Image>().color = Color.red; //WyraŸnie pokazujemy u¿ytkownikowi, ¿e nie ma ju¿ proszku w gaœnicy
        }
    }
}
