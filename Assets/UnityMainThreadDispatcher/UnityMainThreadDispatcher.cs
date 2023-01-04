using UnityEngine;
using UnityEngine.PlayerLoop;

public class UnityMainThreadDispatcher
{
    private static int Tick;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        var subscription = new PlayerLoopSystemSubscription<Update>(Update);
        Application.quitting += subscription.Dispose;
    }

    private static void Update()
    {
        Tick++;
        Debug.Log($"Ticking {Tick} {Time.frameCount}");

        if (Tick != Time.frameCount)
        {
            Debug.LogError("Something went wrong");
        }
    }
}