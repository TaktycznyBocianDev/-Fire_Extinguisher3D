using UnityEngine;
using UnityEngine.UI;


public class FireExtControlScript : MonoBehaviour
{
    [Header("Slider that will control fire extinguisher")]
    [SerializeField] Slider slider;

    private void Update()
    {
        ControlFE();
    }

    private void ControlFE()
    {
        transform.position = new Vector3(transform.position.x, slider.value, transform.position.z);
    }

}
