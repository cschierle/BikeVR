using System;

public class IRCharacteristic : BaseCharacteristic<int>
{
    public IRCharacteristic() : base("11111111-1111-4b23-9358-f235b978d07c", 0)
    {
    }

    public override void UpdateValue(byte[] bytes)
    {
        if (bytes != null)
        {
            this.Value = BitConverter.ToInt16(bytes, 0);
        }
    }
}
