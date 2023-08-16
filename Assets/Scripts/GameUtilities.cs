using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class GameUtilities
{
    public static void Swap<T>(ref T value1, ref T value2)
    {
        T temp = value1;
        value1 = value2;
        value2 = temp;
    }

    public static void Stop(this Tween tween)
    {
        if (tween != null)
            tween.Kill();
    }

    public static bool GetRandomOnRange(float min, float max)
    {
        float value = Random.Range(0, 101);
        return value >= min && value <= max;
    }
}
