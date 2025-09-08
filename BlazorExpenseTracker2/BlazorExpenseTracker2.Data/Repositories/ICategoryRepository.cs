using BlazorExpenseTracker2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker2.Data.Repositories
{
    public interface ICategoryRepository
    {
        //CRUD

        //vamos a intentar que todo el proyecto sea asincrono - es a lo que blazor promueve
        //para poder usar los metodos asincronos se debe poner Task dado que de otro modo cuando trate de acceder a ellos me lo va a pedir

        //este metodo va a traer todoas las categorias activas
        Task<IEnumerable<Category>> GetAllCategories();

        //este solo va a traer una categoria - solo devuelve una
        Task<Category> GetCategoryDetails(int id);
        //insertar objetos
        Task<bool> InsertCategories(Category category);
        //Modificar
        Task<bool> UpdateCategories(Category category);
        //borrar elementos
        Task<bool> DelateCategories(int id);
    }
}
