namespace SushiManager.Exceptions
{
    public class NoActiveOrderFound : Exception
    {
        public NoActiveOrderFound() : base("Nessun ordine attivo per questo utente!")
        {
        }

        public NoActiveOrderFound(string message) : base(message)
        {
        }

        public NoActiveOrderFound(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
