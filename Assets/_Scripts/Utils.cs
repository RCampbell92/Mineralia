using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static float Round(float number, float decPlaces)
    {
        number *= Mathf.Pow(10, decPlaces);
        number = Mathf.Round(number);
        number /= Mathf.Pow(10, decPlaces);

        return number;
    }
}
