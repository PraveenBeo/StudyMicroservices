namespace Catalog.API.Exceptions
{
    public class BadRequestExcetpion : Exception
    {
        public BadRequestExcetpion(string message)
            : base(message)
        {
        }
        public BadRequestExcetpion(string message, string details)
            : base(message)
        {
            Details = details;
        }
        public string? Details { get; }
    }
}
