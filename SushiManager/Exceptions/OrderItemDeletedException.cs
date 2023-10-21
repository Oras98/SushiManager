namespace SushiManager.Exceptions
{
    public class OrderItemDeletedException : Exception
    {
        public OrderItemDeletedException() : base("Il prodotto è già stato canellato dall'ordine!")
        {
        }

        public OrderItemDeletedException(string message) : base(message)
        {
        }

        public OrderItemDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
