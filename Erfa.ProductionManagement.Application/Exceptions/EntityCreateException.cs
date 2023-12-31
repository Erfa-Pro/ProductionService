namespace Erfa.ProductionManagement.Application.Exceptions
{
    public class EntityCreateException : Exception
    {
        public EntityCreateException(string name, object key)
            : base($"{name} ({key}) not modified")
        {
        }
    }
}
