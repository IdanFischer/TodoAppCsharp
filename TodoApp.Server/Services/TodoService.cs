using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Server.Models;

namespace TodoApp.Server.Services
{
public class TodoService
{
    private readonly IMongoCollection<Tasks> _todoCollection;

    public TodoService(IOptions<TodoDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _todoCollection = database.GetCollection<Tasks>(settings.Value.TodoCollectionName);
    }

    public async Task<List<Tasks>> GetAsync() =>
        await _todoCollection.Find(todo => true).ToListAsync();

    public async Task<Tasks> GetAsync(string id) =>
        await _todoCollection.Find(todo => todo.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Tasks todo) =>
        await _todoCollection.InsertOneAsync(todo);

    public async Task UpdateAsync(string id, Tasks todo) =>
        await _todoCollection.ReplaceOneAsync(t => t.Id == id, todo);

    public async Task RemoveAsync(string id) =>
        await _todoCollection.DeleteOneAsync(t => t.Id == id);
}

}

