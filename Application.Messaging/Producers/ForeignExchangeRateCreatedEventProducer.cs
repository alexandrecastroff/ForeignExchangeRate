namespace Application.Messaging.Producers
{
    using Confluent.Kafka;
    using CrossInfrastructure.Kafka;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;

    public class ForeignExchangeRateCreatedEventProducer : IForeignExchangeRateCreatedEventProducer
    {
        private readonly KafkaSettings _configuration;

        private readonly IProducer<Null, string> _producer;

        public ForeignExchangeRateCreatedEventProducer(IOptions<KafkaSettings> configuration)
        {
            _configuration = configuration.Value;

            var producerconfig = new ProducerConfig
            {
                BootstrapServers = _configuration.BootstrapServers
            };

            _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
        }

        public async Task ProduceAsync(string topic, string message)
        {
            var kafkamessage = new Message<Null, string> { Value = message, };

            await _producer.ProduceAsync(topic, kafkamessage);
        }
    }
}
