using BlazorExpenseTracker2.Model;
using BlazorExpenseTracker2.UI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorExpenseTracker2.UI.Services
{
    //Desde el servicio tenemos que mandar los HTTPRequest
    //son los que van a viajar a la api, la api los toma y ara los procesos que prograamos y luego nos manda una respuesta
    public class CategoryService : ICategoryService
    {
        //Pra realizar eso vamos a usar una clase que nos da .net core que se llama
        private readonly HttpClient _httpClient;
        //que tenga algun valor, vamos a crear un constructor y por medio de inyeccion de independencias
        //esta clase va a viajar a startup donde configuramos los servicios
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DelateCategories(int id)
        {
            await _httpClient.DeleteAsync($"Api/Category/{id}");
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            //vamos a mappear el resultado que es un Json, dado que es un httpresponse
            return await JsonSerializer.DeserializeAsync<IEnumerable<Category>>
                (
                    await _httpClient.GetStreamAsync($"Api/Category"),
                    //hacemos lo siguiente por si hay minusculas o mayusculas y no las coja, asi se mappea todo
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true}
                );
        }

        public async Task<Category> GetCategoryDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Category>(
                    await _httpClient.GetStreamAsync($"Api/Category/{id}"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true}
                );
        }

        public async Task SaveCategories(Category category)
        {
            var categoryJson = new StringContent(JsonSerializer.Serialize(category), // Encoding.UTF8);
                //hasta arriba iba a funcionar de forma correcta hacemos esto para mayr seguridad
                Encoding.UTF8, "application/Json");

            //si es 0 es un insert
            if (category.Id == 0)
            {
                await _httpClient.PostAsync("Api/Category/", categoryJson);
            }
            else
            // es un update
            {
                await _httpClient.PutAsync("Api/Category/", categoryJson);

            }
        }
    }
}
