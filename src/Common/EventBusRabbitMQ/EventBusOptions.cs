namespace EventBusRabbitMQ
{
    public class EventBusOptions
    {
        public const string EventBus = "EventBus";

        public string SubscriptionClientName { get; set; }
        public int EventBusRetryCount { get; set; }
        public string EventBusUserName { get; set; }
        public string EventBusPassword { get; set; }
        public string EventBusConnection { get; set; }
    }
}
