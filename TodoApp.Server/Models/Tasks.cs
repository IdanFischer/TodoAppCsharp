using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApp.Server.Models
{
    public class Tasks
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty; // default to empty string

        public string Title { get; set; } = string.Empty; // default to empty string

        public string Description { get; set; } = string.Empty; // default to empty string

        // this will track if the task is completed or not

        // by default, when a task is created, it will be set to false

        public bool IsComplete { get; set; } = false; // default to false
    }
}
