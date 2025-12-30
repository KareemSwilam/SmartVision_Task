using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Validations
{
    public class ValidationFilter<T> : IAsyncActionFilter
    {
        private readonly IValidator<T> _validator;
        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var model = context.ActionArguments.Values.OfType<T>().FirstOrDefault();
            if (model != null)
            {
                var result = await _validator.ValidateAsync(model);
                if (!result.IsValid)
                {
                    var error = result.Errors.Select(e => e.ErrorMessage).ToList();
                    context.Result = new BadRequestObjectResult(new
                    {
                        Error = error,
                        StatusCodes = 400
                    });
                    return;
                }
                await next();
            }

        }
    }
}
