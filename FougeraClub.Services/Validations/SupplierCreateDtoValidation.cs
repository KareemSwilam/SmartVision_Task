using FluentValidation;
using FougeraClub.Services.DTOs.SupplierDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Validations
{
    public class SupplierCreateDtoValidation: AbstractValidator<SupplierCreateDto>
    {
        public SupplierCreateDtoValidation()
        {
            RuleFor(s =>  s.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(s => s.Description).NotEmpty().WithMessage("Description is Required");
            RuleFor(s => s.Email).NotEmpty().WithMessage("Email must added")
                                 .EmailAddress().WithMessage("Email must contain @ Sign");
        }
    }
}
