namespace CrossInfrastructure.Kafka
{
    public interface IForeignExchangeRateCreatedEventProducer
    {
        Task ProduceAsync(string topic, string message);
    }
}
