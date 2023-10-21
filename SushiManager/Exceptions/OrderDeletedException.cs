namespace SushiManager.Exceptions
{
    public class OrderDeletedException: Exception
    {
        public OrderDeletedException() : base("L'ordine cui appartiene questo prodotto è stato cancellato!")
        {
        }

        public OrderDeletedException(string message) : base(message)
        {
        }

        public OrderDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
