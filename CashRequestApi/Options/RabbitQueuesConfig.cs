namespace CashRequestApi.Options
{
    public class RabbitQueuesConfig
    {
        public static string Section { get; } = "RabbitMQ:Queues";

        public string CreateRequestCommandQueue { get; set; } = string.Empty;
        public string GetRequestStatusByIdQueue { get; set; } = string.Empty;
        public string GetRequestStatusByClientIdAndDepAddressQueue { get; set; } = string.Empty;
    }
}
