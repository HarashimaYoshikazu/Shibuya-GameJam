using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculator
{
    private const int MIN_VALUE = -1;
    private const int MAX_VALUE = 1;
    private const int OFFSET = 1;

    public static float RandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }

    public static float RandomValue()
    {
        var value = 0;
        var random = Random.Range(MIN_VALUE, MAX_VALUE + OFFSET);
        if (random == MIN_VALUE) value = MIN_VALUE;
        else if (random == MAX_VALUE) value = MAX_VALUE;
        return value;
    }
}
