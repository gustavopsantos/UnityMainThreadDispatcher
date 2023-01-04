using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

public class UnityMainThreadDispatcher
{
    private static int Tick;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        Subscribe();
        Application.quitting += Unsubscribe;
    }

    private static void Subscribe()
    {
        var loop = PlayerLoop.GetCurrentPlayerLoop();
        ref var updateSystem = ref loop.Find<Update>();
        updateSystem.updateDelegate += Update;
        PlayerLoop.SetPlayerLoop(loop);
    }

    private static void Unsubscribe()
    {
        var loop = PlayerLoop.GetCurrentPlayerLoop();
        ref var updateSystem = ref loop.Find<Update>();
        updateSystem.updateDelegate -= Update;
        PlayerLoop.SetPlayerLoop(loop);
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