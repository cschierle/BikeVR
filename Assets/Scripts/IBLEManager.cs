public interface IBLEManager
{
    T GetCharacteristic<T>() where T : Characteristic;
}