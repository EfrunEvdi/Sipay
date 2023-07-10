using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SipayTask1.Entities;

namespace SipayTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IValidator<Customer> _validator;

        public CustomerController(IValidator<Customer> validator)
        {
            _validator = validator;
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            ValidationResult result = _validator.Validate(customer);

            if (!result.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
            }

            return StatusCode(StatusCodes.Status201Created, "Customer created successfully!");
        }
    }
}
