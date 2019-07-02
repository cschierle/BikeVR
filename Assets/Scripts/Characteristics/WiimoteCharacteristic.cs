using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiimoteCharacteristic : BaseCharacteristic<int>
{
    public WiimoteCharacteristic() : base("33333333-3333-4b23-9358-f235b978d07c", 0)
    {
    }

    public override void UpdateValue(byte[] bytes)
    {
        if (bytes != null)
        {
            Value = BitConverter.ToInt16(bytes, 0);
        }
    }
}
