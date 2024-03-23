namespace CashRequestApi.Options
{
    public class RabbitQueuesConfig
    {
        public static string Section { get; set; } = "RabbitMQ::Queues";

        public string CreateRequestCommandQueue { get; set; } = string.Empty;
    }
}
