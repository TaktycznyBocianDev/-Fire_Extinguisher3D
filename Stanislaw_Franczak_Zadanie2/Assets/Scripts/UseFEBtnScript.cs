using UnityEngine;
using UnityEngine.EventSystems;

public class UseFEBtnScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool buttonPressed;


    private void OnEnable()
    {
        EventManager.EndOfFiller += ThisIsTheEnd;
    }

    private void OnDisable()
    {
        EventManager.EndOfFiller -= ThisIsTheEnd;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.UsingFEFuncStarts();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventManager.EndOfUsingFEFunc();
    }

    private void ThisIsTheEnd()
    {
        Destroy(gameObject);
    }
}
