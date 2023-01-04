using System;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace UnityMainThreadDispatcher
{
    public static class Dispatcher
    {
        private static readonly ConcurrentQueue<Action> _actionQueue = new ConcurrentQueue<Action>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var subscription = new PlayerLoopSystemSubscription<Update>(Update);
            Application.quitting += subscription.Dispose;
        }

        private static void Update()
        {
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
}