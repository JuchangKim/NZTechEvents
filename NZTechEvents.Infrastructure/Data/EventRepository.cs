using Microsoft.Azure.Cosmos;
using NZTechEvents.Core.Entities;

namespace NZTechEvents.Infrastructure.Data
{
    public class EventRepository
    {
        private readonly Container _container;

        public EventRepository(CosmosClient client, string databaseName, string containerName)
        {
            _container = client.GetContainer(databaseName, containerName);
        }

        public async Task<Event> CreateEventAsync(Event evt)
        {
            var response = await _container.CreateItemAsync(evt, new PartitionKey(evt.EventId));
            return response.Resource;
        }

        public async Task<Event?> GetEventAsync(string eventId)
        {
            try
            {
                var response = await _container.ReadItemAsync<Event>(eventId, new PartitionKey(eventId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async IAsyncEnumerable<Event> GetAllEventsAsync()
        {
            var query = _container.GetItemQueryIterator<Event>("SELECT * FROM c");
            while (query.HasMoreResults)
            {
                foreach (var evt in await query.ReadNextAsync())
                {
                    yield return evt;
                }
            }
        }

        // And so on...
    }
}
