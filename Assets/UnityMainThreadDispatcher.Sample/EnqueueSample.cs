using System;
using System.Threading;
using UnityEngine;

namespace UnityMainThreadDispatcher.Sample
{
    public class EnqueueSample : MonoBehaviour
    {
        private bool _quitting;
        private Thread _thread;
    
        private void Start()
        {
            _thread = new Thread(RunsInAnotherThread);
            _thread.Start();
        }

        private void OnDestroy()
        {
            _quitting = true;
        }

        private void RunsInAnotherThread()
        {
            while (!_quitting)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(16));

                Dispatcher.Enqueue(() =>
                {
                    transform.position = new Vector2(Mathf.Cos(Time.time), Mathf.Sin(Time.time));
                });
            }
        }
    }
}