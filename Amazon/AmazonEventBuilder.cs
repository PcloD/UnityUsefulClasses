using Amazon.MobileAnalytics.MobileAnalyticsManager;

public class AmazonEventBuilder
{
    private CustomEvent customEvent;

    public AmazonEventBuilder(string eventType)
    {
        customEvent = new CustomEvent(eventType);
    }

    public AmazonEventBuilder AddAttribute(string attributeName, string attributeValue)
    {
        customEvent.AddAttribute(attributeName, attributeValue);
        return this;
    }

    public AmazonEventBuilder AddMetric(string metricName, double metricValue)
    {
        customEvent.AddMetric(metricName, metricValue);
        return this;
    }

    public CustomEvent GetEvent()
    {
        return customEvent;
    }
}