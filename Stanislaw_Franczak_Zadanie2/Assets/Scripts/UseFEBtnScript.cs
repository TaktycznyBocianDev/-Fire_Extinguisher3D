using UnityEngine;
using UnityEngine.EventSystems;

public class UseFEBtnScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /*
     * Aby przycisk móg³ wykonywaæ swoje funkcje
     * implementujemy odpowiednie Interfejsy oraz nadpisujemy funkcje tak, 
     * aby przyciœniêcie przycisku uruchamia³o odpowiedni event.
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
    //U¿ycie tego eventu w momencie zniszczenia zbêdnego przycisku sprawia ¿e emisja ognia nie zmiejsza siê do nieskoñczonoœci
    private void OnDestroy()
    {
        EventManager.StopUsingFireExtinguisherFunc();
    }
    //Gdy skoñczy siê wype³nienie w gaœnicy, przycisk nie jest potrzebny
    private void ThisIsTheEnd()
    {     
        Destroy(gameObject);
    }    
}
