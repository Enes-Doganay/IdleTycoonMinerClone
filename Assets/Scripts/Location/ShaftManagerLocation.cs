using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaftManagerLocation : BaseManagerLocation
{
    public static Action<Shaft, ShaftManagerLocation> OnBoost;
    private Shaft shaft;
    private void Start()
    {
        shaft = GetComponent<Shaft>();
    }
    public override void RunBoost()
    {
        OnBoost?.Invoke(shaft, this);
    }
}
