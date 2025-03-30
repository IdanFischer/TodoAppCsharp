namespace TodoApp.Server.Models
{
    public class TodoDatabaseSettings
    {  
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string TodoCollectionName { get; set; } 
    }
}
