using System;
using UnityEngine.LowLevel;

namespace UnityMainThreadDispatcher
{
    internal class PlayerLoopSystemSubscription<T> : IDisposable
    {
        private readonly Action _callback;

        public PlayerLoopSystemSubscription(Action callback)
        {
            _callback = callback;
            Subscribe();
        }

        private void Invoke()
        {
            _callback.Invoke();
        }

        private void Subscribe()
        {
            var loop = PlayerLoop.GetCurrentPlayerLoop();
            ref var system = ref loop.Find<T>();
            system.updateDelegate += Invoke;
            PlayerLoop.SetPlayerLoop(loop);
        }

        private void Unsubscribe()
        {
            var loop = PlayerLoop.GetCurrentPlayerLoop();
            ref var system = ref loop.Find<T>();
            system.updateDelegate -= Invoke;
            PlayerLoop.SetPlayerLoop(loop);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}