using BlazorExpenseTracker2.Data.Data;
using BlazorExpenseTracker2.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker2.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {

        //Configuracion de la cadena de conexion
        private SQLConfiguration _connectionString;

        public ExpenseRepository(SQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConection()
        {
            return new SqlConnection(_connectionString.ConectionString);
        }
        //Fin

        public async Task<bool> DeleteExpense(int id)
        {
            var db = dbConection();

            var sql = @"DELETE Expenses 
                        WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql, new { id });

            return result > 0; // Sussess
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            var db = dbConection();

            var sql = @"SELECT e.Id, Amount, CategoryId, ExpenseType, TransactionDate,
                               c.Id, c.Name
                        FROM Expenses e
                        INNER JOIN Categories c ON e.CategoryId = c.Id";

            //dapper
            //cargar los 2 elemento
            var result = await db.QueryAsync<Expense, Category, Expense>(sql,
                //para combinar los elemnetos
                (
                (Expense, Category) =>
                {
                    Expense.Category = Category;
                    return Expense;
                }), new { }, splitOn: "Id"
                );

            return result;
        }

        public async Task<Expense> GetExpenseDetails(int id)
        {
            var db = dbConection();

            var sql = @"SELECT Id, Amount, CategoryId, ExpenseType, TransactionDate
                        FROM Expenses
                        WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = id });
        }

        public async Task<bool> InsertExpenseDetails(Expense expense)
        {
            var db = dbConection();

            var sql = @"INSERT INTO Expenses(Amount, CategoryId, ExpenseType, TransactionDate)
                VALUES(@Amount, @CategoryId, @ExpenseType, @TransactionDate)";


            var result = await db.ExecuteAsync(sql,
                new
                {
                    expense.Amount,
                    expense.CategoryId,
                    expense.ExpenseType,
                    expense.TransactionDate
                });

            return result > 0;
        }

        public async Task<bool> UpdateExpense(Expense expense)
        {
            var db = dbConection();

            var sql = @"UPDATE Expenses SET 
                       Amount = @Amount, CategoryID = @CategoryId, ExpenseType = @ExpenseType, TransactionDate = @TransactionDate
                        WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql,
                new { expense.Id, expense.Amount, expense.CategoryId, expense.ExpenseType, expense.TransactionDate });

            return result > 0;
        }
    }
}
