using System.ComponentModel.DataAnnotations;

namespace BlazorExpenseTracker.Model
{
    public class Category
    {
        public int Id { get; set; }
        //este Required lo que hace es que sea obligatorio ese campo, el cual es llama el decorador
        // AllowEmptyStrings no permite valores vacios -> si sucede va a mostar el ErrorMessage que es el error
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name is required")]
        //esta propiedad lo que hace es que sea una direccion de imail
        //[EmailAddress]
        //[StringLength (50)]
        public string Name { get; set; }
    }
}