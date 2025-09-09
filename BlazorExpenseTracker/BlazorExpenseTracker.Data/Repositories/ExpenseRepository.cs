using BlazorExpenseTracker.Model;
using Dapper;
using System.Data.SqlClient;

namespace BlazorExpenseTracker.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private SqlConfiguration _connectionString;

        public ExpenseRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT e.Id, Amount, CategoryId, ExpenseType, TransactionDate, 
                               c.Id, c.Name
                        FROM Expenses e
                            INNER JOIN Categories c ON e.CategoryId = c.Id ";

            var result = await db.QueryAsync<Expense, Category, Expense>(sql,
                ((expense, category) =>
                {
                    expense.Category = category;
                    return expense;
                }), new { }, splitOn: "Id");

            return result;
        }

        public async Task<Expense> GetExpenseDetails(int id)
        {
            var db = dbConnection();

            var sql = @" SELECT Id, Amount, CategoryId, ExpenseType, TransactionDate
                          FROM Expenses 
                          WHERE Id = @Id ";

            return await db.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = id });
        }

        public async Task<bool> InsertExpenseDetails(Expense expense)
        {
            try
            {
                using var db = dbConnection();

                var sql = @"INSERT INTO Expenses(Amount, CategoryId, ExpenseType, TransactionDate)
                    VALUES(@Amount, @CategoryId, @ExpenseType, @TransactionDate)";

                var result = await db.ExecuteAsync(sql, new
                {
                    expense.Amount,
                    expense.CategoryId,
                    expense.ExpenseType,
                    expense.TransactionDate
                });

                return result > 0;
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error con tu logger (NLog, Serilog, Console, etc.)
                Console.WriteLine($"Error en InsertExpenseDetails: {ex.Message}");
                // También puedes relanzar si quieres que lo maneje más arriba
                throw;
            }
        }

        public async Task<bool> UpdateExpense(Expense expense)
        {
            try
            {
                using var db = dbConnection();

                var sql = @"UPDATE Expenses 
                    SET Amount = @Amount, CategoryId = @CategoryId, ExpenseType = @ExpenseType,
                        TransactionDate = @TransactionDate 
                    WHERE Id = @Id";

                var result = await db.ExecuteAsync(sql, new
                {
                    expense.Id,
                    expense.Amount,
                    expense.CategoryId,
                    expense.ExpenseType,
                    expense.TransactionDate
                });

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateExpense: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteExpense(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE Expenses WHERE Id = @Id ";

            var result = await db.ExecuteAsync(sql, new { id });

            return result > 0;
        }
    }
}