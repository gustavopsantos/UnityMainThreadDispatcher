using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UnityMainThreadDispatcher
{
    private static int Tick;

    private static readonly ConcurrentQueue<Action> _actionQueue = new ConcurrentQueue<Action>();

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

        while (_actionQueue.TryDequeue(out var action))
        {
            action?.Invoke();
        }
    }

    public static void Enqueue(Action action)
    {
        _actionQueue.Enqueue(action);
    }
}