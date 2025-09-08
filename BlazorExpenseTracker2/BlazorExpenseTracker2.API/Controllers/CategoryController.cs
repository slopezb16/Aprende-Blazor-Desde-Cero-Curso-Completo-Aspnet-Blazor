using BlazorExpenseTracker2.Data.Repositories;
using BlazorExpenseTracker2.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorExpenseTracker2.API.Controllers
{
    //se crea una rutu que es la ruta para acceder al componente
    [Route("Api/[controller]")]
    // esto [controller] remplaza y pone o es igual a como se llama CategoryController

    //Le indicamos que se comporte como un api
    [ApiController]
    // esto sera un CRUD
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        //Se hace el constructor
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //este decorador nos permite acceder al metodo
        [HttpGet]
        // IActionResult es lo que comunica la vista y el controlador -> va hacer el resultado del metodo get
        public async Task<IActionResult> GetAllCategories()
        {
            //el OK crea un HTTP response o algo asi parecido
            return Ok(await _categoryRepository.GetAllCategories());
            //este metodo ok es un metodo que provee el framewor .NET para convertir el resultado de la base de datos en un http response - pone la cabecera el cuerpo y todo como debe de ser
        }

        // este va a tener un ajuste hay que agregarle los parametrps
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryDetails(int id)
        {
            return Ok(await _categoryRepository.GetCategoryDetails(id));
        }

        //ahora realizaremos un metodo post, dado que se van hacer modificaciones en la base se van hacer insert o update
        // FromBody indica desde donde debe sacar la informacion
        // Category category indica que es lo que quiere que se devuelva
        //decorador HttpPost
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            //Se agregan las validaciones

            if (category == null)
            {
                // Crea un response indidando un codigo 400
                return BadRequest();
            }

            //validamos que no este vacio el campo
            if (category.Name.Trim() == string.Empty)
            {
                //como se puede acceder desde postman debemos poner la validacion de que no este vacio
                //indicamos que hay un error en el modelo AddModelError -> y le indicamos donde esta el error y un mensage
                ModelState.AddModelError("Name", "Category Name shouldn't be empty");
            }

            //revisamos la validacion de arriba para enviar el mensaje
            if (!ModelState.IsValid)
            {
                //volvemos a crear el response pero le agregamos algo mas
                //agregamos el mesage - para darle mas informacion al usuario de que esta malo
                return BadRequest(ModelState);
            }

            //ahora bien ya aca esta todo bien, ya podemos ingresar
            var created = await _categoryRepository.InsertCategories(category);

            //en la variable creat tenemos el bool que se creo en la interfaz
            return Created("created", created);
        }

        //este no es un post si no que es un put dado que es una modificacion
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            //Se agregan las validaciones

            if (category == null)
            {
                // Crea un response indidando un codigo 400
                return BadRequest();
            }

            //validamos que no este vacio el campo
            //el Trim() remueve los espacios
            if (category.Name.Trim() == string.Empty)
            {
                //como se puede acceder desde postman debemos poner la validacion de que no este vacio
                //indicamos que hay un error en el modelo AddModelError -> y le indicamos donde esta el error y un mensage
                ModelState.AddModelError("Name", "Category Name shouldn't be empty");
            }

            //revisamos la validacion de arriba para enviar el mensaje
            if (!ModelState.IsValid)
            {
                //volvemos a crear el response pero le agregamos algo mas
                //agregamos el mesage - para darle mas informacion al usuario de que esta malo
                return BadRequest(ModelState);
            }

            //a comparacion con el insert est va a cambiar un poco
            await _categoryRepository.UpdateCategories(category);

            //Si llega hasta aca es que esta bien
            return NoContent(); // Succes
        }

        //decorador con HttpDelete para eliminar
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            await _categoryRepository.DelateCategories(id);
            return NoContent(); //Success
        }

        //Despues de todo esto nos vamos a configurar el el starup -> para organizar la inyeccion de dependencias

    }
}
