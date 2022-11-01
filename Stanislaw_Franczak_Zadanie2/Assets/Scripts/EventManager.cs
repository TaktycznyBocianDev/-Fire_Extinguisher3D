using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void UsingFE();
    public static event UsingFE StartUsingFireExtinguisher;
    public static event UsingFE StopUsingFireExtinguisher;
    public static event UsingFE FillerHasEnded;

    public static void StartUsingFireExtinguisherFunc()
    {
        StartUsingFireExtinguisher?.Invoke();
    }
    public static void StopUsingFireExtinguisherFunc()
    {
        StopUsingFireExtinguisher?.Invoke();
    }
    public static void FilerEndsFunc()
    {
        FillerHasEnded?.Invoke();
    }
}
