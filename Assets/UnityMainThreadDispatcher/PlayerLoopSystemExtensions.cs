using System;
using UnityEngine.LowLevel;

namespace UnityMainThreadDispatcher
{
    internal static class PlayerLoopSystemExtensions
    {
        public static ref PlayerLoopSystem Find<T>(this PlayerLoopSystem root)
        {
            for (int i = 0; i < root.subSystemList.Length; i++)
            {
                if (root.subSystemList[i].type == typeof(T))
                {
                    return ref root.subSystemList[i];
                }
            }

            throw new Exception($"System of type '{typeof(T).Name}' not found inside system '{root.type.Name}'.");
        }
    }
}