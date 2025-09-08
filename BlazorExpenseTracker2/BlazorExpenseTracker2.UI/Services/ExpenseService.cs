using BlazorExpenseTracker2.Model;
using BlazorExpenseTracker2.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorExpenseTracker2.UI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly HttpClient _httpClient;
        public ExpenseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteExpense(int id)
        {
            await _httpClient.DeleteAsync($"Api/Expense/{id}");
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Expense>>(
                    await _httpClient.GetStreamAsync($"Api/Expense"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
            }
            catch (Exception ex)
            {
                // Registra o imprime la excepción para obtener detalles sobre el problema.
                Console.WriteLine(ex.Message);
                throw; // Lanza la excepción nuevamente si es necesario.
            }
        }

        public async Task<Expense> GetExpenseDetails(int id)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<Expense>
                    (
                        await _httpClient.GetStreamAsync($"Api/Expense/{id}"),
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                    );
            }
            catch (Exception ex)
            {
                // Registra o imprime la excepción para obtener detalles sobre el problema.
                Console.WriteLine(ex.Message);
                throw; // Lanza la excepción nuevamente si es necesario.
            }
        }

        public async Task SaveExpense(Expense expense)
        {
            var ExpenseJson = new StringContent(JsonSerializer.Serialize(expense), 
                Encoding.UTF8, "application/Json");

            if(expense.Id == 0)
            {
                await _httpClient.PostAsync("Api/Expense", ExpenseJson); //insert expense
            }
            else
            {
                await _httpClient.PutAsync("Api/Expense", ExpenseJson); //Update expense
            }
        }
    }
}
