namespace Shared
{
    public static class RabbitMqConfig
    {
        public const string HostAddress = "rabbitmq://localhost";
        public const string Username = "guest";
        public const string Password = "guest";

        public static class Queues
        {
            public const string Invoices = "poc.invoices";
        }
    }
}