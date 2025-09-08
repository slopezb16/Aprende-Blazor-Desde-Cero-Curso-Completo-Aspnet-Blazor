using BlazorExpenseTracker2.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorExpenseTracker2.Model
{
    public class Expense //: IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount need to be greater than 0")]
        public decimal Amount { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [ExpenseTransactionDateValidator(DayInTheFuture = 30)]
        public DateTime TransactionDate { get; set; }
        public ExpenseType ExpenseType { get; set; }

        public event Action OnSelectedExpenseChanged;

        public void SelectedExpenseChanged(Expense expense) 
        { 
            Id = expense.Id;
            TransactionDate = expense.TransactionDate;
            Amount = expense.Amount;
            ExpenseType = expense.ExpenseType;
            CategoryId = expense.CategoryId;

            NotifySelectedExpenseChanged();
        }

        private void NotifySelectedExpenseChanged()
        {
            OnSelectedExpenseChanged.Invoke();
        }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var error = new List<ValidationResult>();

        //    if(ExpenseType == ExpenseType.Income && Amount < 0)
        //    {
        //        error.Add(new ValidationResult("Income can't be lasser that zero",
        //            new[] { nameof(Amount) }));
        //    }
        //    else if (ExpenseType == ExpenseType.Expense && Amount > 0) 
        //    {
        //        error.Add(new ValidationResult("Expense can't be greater that zero",
        //           new[] { nameof(Amount) }));
        //    }
        //    return error;
        //}
    }
}
