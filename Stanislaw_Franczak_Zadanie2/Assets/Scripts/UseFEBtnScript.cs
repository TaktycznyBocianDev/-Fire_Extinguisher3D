using UnityEngine;
using UnityEngine.EventSystems;

public class UseFEBtnScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /*
     * Aby przycisk m�g� wykonywa� swoje funkcje
     * implementujemy odpowiednie Interfejsy oraz nadpisujemy funkcje tak, 
     * aby przyci�ni�cie przycisku uruchamia�o odpowiedni event.
     */

    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.StartUsingFireExtinguisherFunc();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        EventManager.StopUsingFireExtinguisherFunc();
    }
    private void OnEnable()
    {
        EventManager.FillerHasEnded += ThisIsTheEnd;
    }
    private void OnDisable()
    {
        EventManager.FillerHasEnded -= ThisIsTheEnd;
    }
    //U�ycie tego eventu w momencie zniszczenia zb�dnego przycisku sprawia �e emisja ognia nie zmiejsza si� do niesko�czono�ci
    private void OnDestroy()
    {
        EventManager.StopUsingFireExtinguisherFunc();
    }
    //Gdy sko�czy si� wype�nienie w ga�nicy, przycisk nie jest potrzebny
    private void ThisIsTheEnd()
    {     
        Destroy(gameObject);
    }    
}
