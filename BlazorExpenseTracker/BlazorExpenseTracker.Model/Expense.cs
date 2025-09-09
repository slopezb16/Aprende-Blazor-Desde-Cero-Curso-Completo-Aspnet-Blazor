using BlazorExpenseTracker.Model.Validation;
using System.ComponentModel.DataAnnotations;

namespace BlazorExpenseTracker.Model
{
    public class Expense //: IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount needs to be greater than 0")]
        public decimal Amount { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        [ExpenseTransactionDateValidator(DaysInTheFuture = 30)]
        public DateTime TransactionDate { get; set; }
        public ExpenseType ExpenseType { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var error = new List<ValidationResult>();

        //    if (ExpenseType == ExpenseType.Income && Amount < 0)
        //    {
        //        error.Add(new ValidationResult("Income can't be lesser that zero.",
        //            new[] { nameof(Amount) }));
        //    }
        //    else if (ExpenseType == ExpenseType.Expense && Amount > 0)
        //    {
        //        error.Add(new ValidationResult("Expense can't be greater that zero.",
        //            new[] { nameof(Amount) }));
        //    }

        //    return error;
        //}
    }
}
