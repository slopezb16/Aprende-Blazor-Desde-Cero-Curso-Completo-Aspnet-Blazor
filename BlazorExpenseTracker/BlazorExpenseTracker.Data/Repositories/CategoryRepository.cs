using BlazorExpenseTracker.Data.Data;
using BlazorExpenseTracker.Model;
using Dapper;
using System.Data.SqlClient;

namespace BlazorExpenseTracker.Data.Repositories
{
    //creamos la otra clase para implementar la interfaz
    //para eso ponemos : y el nombre de la interfaz -> con esto nos aparecera un error le damos en la lupa y crea todos los metoso que teniamos en la otra
    public class CategoryRepository : ICategoryRepository
    {
        //estos metodos los creamos nosotros
        //se crean para traer la cadena de coneccion
        private SqlConfiguration _connectionString;

        public CategoryRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        //lo usamos para conectarnos a la base o al Cliente SQL
        protected SqlConnection dbConnection()
        {
            //return dbConection();
            return new SqlConnection(_connectionString.ConnectionString);
            //cada ves que se llame este metodo sea  capas de crear o abrir una conexion con la base de datos
        }

        //Se crean automaticamente
        //se crea con daper

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var db = dbConnection();

            var sql = @" SELECT Id, Name 
                         FROM Categories ";

            return await db.QueryAsync<Category>(sql, new { });
        }

        public async Task<Category> GetCategoryDetails(int id)
        {
            //se trea la conexion a la base
            var db = dbConnection();

            var sql = @" SELECT Id, Name 
                         FROM Categories
                         WHERE Id = @Id ";
            //ahora usamos daper -> en el proyecto BlazorExpenseTracker2.Data en el administrador de nuget lo instalamos Dapper

            //aca usamos dapper que es con el Query -> 
            return await db.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
            // se agrega Category el tipo para que Dapper lo pueda mappear
            //await es para que el metodo esper un poco - > le indica al programa que siga con la secuencia y cuando lo tenga listo le avisa automaticamente
        }

        public async Task<bool> InsertCategory(Category category)
        {
            var db = dbConnection();

            var sql = @" INSERT INTO Categories (Name) 
                         VALUES(@Name) ";

            //Solo necesitamos el primero
            var result = await db.ExecuteAsync(sql, new { category.Name });

            return result > 0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var db = dbConnection();

            var sql = @" UPDATE Categories
                         SET Name = @Name
                         WHERE Id = @Id ";

            //indica la cantidad de filas que fueron afectadas como en SQL
            var result = await db.ExecuteAsync(sql, new { category.Name, category.Id });

            //Solo se afacta una fila porque es lo que enviamos
            return result > 0;
            //si es verdadero es mayor a 0
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE Categories
                        WHERE Id = @Id ";

            //indica la cantidad de filas que fueron afectadas como en SQL
            var result = await db.ExecuteAsync(sql, new { Id = id });

            //Solo se afacta una fila porque es lo que enviamos
            return result > 0;
            //si es verdadero es mayor a 0
        }
    }
}