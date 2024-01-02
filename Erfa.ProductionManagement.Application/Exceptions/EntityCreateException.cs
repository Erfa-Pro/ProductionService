namespace Erfa.ProductionManagement.Application.Exceptions
{
    public class EntityCreateException : Exception
    {
        public EntityCreateException(string name)
            : base($"{name} not created")
        {
        }
    }
}
