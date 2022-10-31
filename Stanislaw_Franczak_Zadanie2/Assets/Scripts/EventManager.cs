using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void UsingFE();
    public static event UsingFE usingFEStart;
    public static event UsingFE EndOfusingFE;
    public static event UsingFE EndOfFiller;
   

    public static void UsingFEFuncStarts()
    {
        usingFEStart?.Invoke();
    }

    public static void EndOfUsingFEFunc()
    {
        EndOfusingFE?.Invoke();
    }

    public static void EndOfFillerFunc()
    {
        EndOfFiller?.Invoke();
    }
}
