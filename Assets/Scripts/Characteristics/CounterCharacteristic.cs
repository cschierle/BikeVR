using System;

public class CounterCharacteristic : BaseCharacteristic<int>
{
    public CounterCharacteristic() : base("99999999-1111-4b23-9358-f235b978d07c", 0)
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
