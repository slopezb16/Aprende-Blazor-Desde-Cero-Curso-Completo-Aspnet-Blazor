using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorExpenseTracker2.Model.Validation
{
    public class ExpenseTransactionDateValidator : ValidationAttribute
    {

        public int DayInTheFuture {  get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime transactionDate;
            //pruebas postman
            //DateTime transactionDate2 = DateTime.Now;

            //validamos que la fecha sea una fecha, que tenga el contexto de una fecha
            if (DateTime.TryParse(value.ToString(), out transactionDate))
            {
                //no permite que sea una fecha menor a la de por defecto
                if (transactionDate == DateTime.MinValue)
                {
                    return new ValidationResult($"Date Shouldn't be empty",
                        new [] {validationContext.MemberName});
                }
                //la fecha no puede ser maños a la de hoy
                else if (transactionDate > DateTime.Now.AddDays(DayInTheFuture))
                {
                    return new ValidationResult($"Date can't be great than today plus {DayInTheFuture}",
                        new[] {validationContext.MemberName});

                }
                return null;
            }
            return new ValidationResult($"Invalid date {DayInTheFuture}",
                    new[] { validationContext.MemberName });

                
        }
    }
}
