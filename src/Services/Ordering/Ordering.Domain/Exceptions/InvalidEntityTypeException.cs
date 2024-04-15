namespace Ordering.Domain.Exceptions
{
    public class InvalidEntityTypeException :ApplicationException
    {
        public InvalidEntityTypeException(string name)
            : base($"Entity \"{name}\" is invalid.")
        {

        }
    }
}
