using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Models.Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown||customer.MembershipTypeId == MembershipType.PayAsYouG0)
                return ValidationResult.Success;
            if (customer.DOB == null)
                return new ValidationResult("This Field is Required");

            var age = DateTime.Today.Year - customer.DOB.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Age must be 18 or Higher");
        }

    }
}
