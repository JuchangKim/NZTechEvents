using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using NZTechEvents.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var response = await _container.CreateItemAsync(evt, new PartitionKey(evt.id));
            return response.Resource;
        }

        // Get Event by ID
        public async Task<Event?> GetEventAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Event>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        // Get All Events
        public async IAsyncEnumerable<Event> GetAllEventsAsync()
        {
            var query = _container.GetItemLinqQueryable<Event>().ToFeedIterator();
            while (query.HasMoreResults)
            {
                foreach (var evt in await query.ReadNextAsync())
                {
                    yield return evt;
                }
            }
        }

        // Upsert Event
        public async Task<Event> UpsertEventAsync(Event evt)
        {
            // If your partition key is /id, pass evt.id to PartitionKey
            var response = await _container.UpsertItemAsync(evt, new PartitionKey(evt.id));
            return response.Resource;
        }

        // Create Comment
        public async Task AddCommentToEvent(string id, string userName, string content)
        {
            var evt = await GetEventAsync(id);
            if (evt == null) return; // or throw an exception

            evt.Comments.Add(new Comment
            {
                id = Guid.NewGuid().ToString(), // Ensure this property is populated
                UserName  = userName,
                Content   = content,
                CommentDate = DateTime.UtcNow,
                EventId = id
            });

            await UpsertEventAsync(evt);
        }
        
        // Update an existing comment
        public async Task UpdateCommentOnEvent(string eventId, string commentId, string newContent)
        {
            var evt = await GetEventAsync(eventId);
            if (evt == null) return;

            // Find the comment
            var existingComment = evt.Comments.FirstOrDefault(c => c.id == commentId);
            if (existingComment == null) return;

            // Update
            existingComment.Content = newContent;
            existingComment.CommentDate = DateTime.UtcNow;

            // Save changes
            await UpsertEventAsync(evt);
        }

        // Delete a comment
        public async Task DeleteCommentOnEvent(string eventId, string commentId)
        {
            var evt = await GetEventAsync(eventId);
            if (evt == null) return;

            var existingComment = evt.Comments.FirstOrDefault(c => c.id == commentId);
            if (existingComment == null) return;

            evt.Comments.Remove(existingComment);

            await UpsertEventAsync(evt);
        }
    }
}
