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
    //creamos la otra clase para implementar la interfaz
    //para eso ponemos : y el nombre de la interfaz -> con esto nos aparecera un error le damos en la lupa y crea todos los metoso que teniamos en la otra
    public class CategoryRepository : ICategoryRepository
    {

        //estos metodos los creamos nosotros
        //se crean para traer la cadena de coneccion
        private SQLConfiguration _connectionString;

        public CategoryRepository(SQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        //lo usamos para conectarnos a la base o al Cliente SQL
        protected SqlConnection dbConection()
        {
            //return dbConection();
            return new SqlConnection(_connectionString.ConectionString);
            //cada ves que se llame este metodo sea  capas de crear o abrir una conexion con la base de datos
        }


        //Se crean automaticamente
        //se crea con daper

        public async Task<bool> DelateCategories(int id)
        {
            var db = dbConection();

            var SQL = "DELETE Categories " +
                      "WHERE Id = @Id";

            var result = await db.ExecuteAsync(SQL, new {Id = id});

            return result > 0;

        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            //se trea la conexion a la base
            var db = dbConection();

            var SQL = @"SELECT Id, Name
                        FROM Categories";
            //ahora usamos daper -> en el proyecto BlazorExpenseTracker2.Data en el administrador de nuget lo instalamos Dapper

            //aca usamos dapper que es con el Query -> 
            return await db.QueryAsync<Category>(SQL, new { });
            // se agrega Category el tipo para que Dapper lo pueda mappear

            //await es para que el metodo esper un poco - > le indica al programa que siga con la secuencia y cuando lo tenga listo le avisa automaticamente

        }

        public async Task<Category> GetCategoryDetails(int id)
        {
            var db = dbConection();

            var SQL = @"SELECT Id, Name
                        FROM Categories
                        WHERE Id = @Id";

            //Solo necesitamos el primero
            return await db.QueryFirstOrDefaultAsync<Category>(SQL, new { Id = id });
        }

        public async Task<bool> InsertCategories(Category category)
        {
            var db = dbConection();

            var SQL = @"INSERT INTO Categories(Name)
                        Values(@Name)";

            //indica la cantidad de filas que fueron afectadas como en SQL
            var result = await db.ExecuteAsync(SQL, new { category.Name });

            //Solo se afacta una fila porque es lo que enviamos
            return result > 0;
            //si es verdadero es mayor a 0
        }

        public async Task<bool> UpdateCategories(Category category)
        {
            var db = dbConection();

            var SQL = @"UPADTE Categories 
                        SET Name = @Name
                        Where Id = @ID";

            //indica la cantidad de filas que fueron afectadas como en SQL
            var result = await db.ExecuteAsync(SQL, new {category.Name, category.Id});

            //Solo se afacta una fila porque es lo que enviamos
            return result > 0;
            //si es verdadero es mayor a 0
        }
    }
}
