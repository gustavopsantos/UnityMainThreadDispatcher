using System;
using UnityEngine.LowLevel;

internal static class PlayerLoopExtensions
{
    public static ref PlayerLoopSystem Find<T>(this PlayerLoopSystem parentSystem)
    {
        for (int i = 0; i < parentSystem.subSystemList.Length; i++)
        {
            if (parentSystem.subSystemList[i].type == typeof(T))
            {
                return ref parentSystem.subSystemList[i];
            }
        }

        throw new Exception($"System of type '{typeof(T).Name}' not found inside system '{parentSystem.type.Name}'.");
    }
}