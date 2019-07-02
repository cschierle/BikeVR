public abstract class Characteristic
{
    public string UUID { get; set; }
    public bool Discovered { get; set; }
    public bool Subscribed { get; set; }
    public abstract void UpdateValue(byte[] bytes);
}
