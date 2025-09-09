namespace BlazorExpenseTracker.Data.Data
{
    public class SqlConfiguration
    {

        public SqlConfiguration(string connectionString) => ConnectionString = connectionString;

        public string ConnectionString { get; }
    }
}