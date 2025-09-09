using BlazorExpenseTracker.Model;

namespace BlazorExpenseTracker.UI.Interfaces
{
    public interface ICategoryService
    {
        //lo que vamos a realizar es traernos desde ICategoryRepository en DATA los metodos. iguales
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryDetails(int id);
        //aca vamos a eliminar 2 metodos los cuales son InsertCategories y UpdateCategories
        //este va a indicar cuando se inserta y cuando se modifica
        Task SaveCategories(Category category);
        //borrar elementos
        Task DelateCategories(int id);
        //termina aca
    }
}