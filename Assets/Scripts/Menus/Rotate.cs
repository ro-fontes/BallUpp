﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumAxis
{
    X,
    Y,
    Z
}

public class Rotate : MonoBehaviour
{
    
    public EnumAxis axis;
    public float speed;

    private void Update()
    {
       switch (axis)
        {
            case EnumAxis.X:
                transform.Rotate(speed, 0, 0);
                break;
            case EnumAxis.Y:
                transform.Rotate(0, speed, 0);
                break;
            case EnumAxis.Z:
                transform.Rotate(0,0 , speed);
                break;
        }
    }

}