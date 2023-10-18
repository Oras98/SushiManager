namespace SushiRestaurant.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound() : base("Combinazione Username/Password non trovata!")
        {
        }

        public UserNotFound(string message) : base(message)
        {
        }

        public UserNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
