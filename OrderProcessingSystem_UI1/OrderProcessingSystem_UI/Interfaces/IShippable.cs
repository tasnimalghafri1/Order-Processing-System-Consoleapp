namespace OrderProcessingSystem_UI.Interfaces
{
    public interface IShippable
    {
        void Ship();
        string GetShippingStatus();
    }
}