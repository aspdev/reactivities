using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Employees;
using Application.Helpers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> List()
        {
            return await _mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> Details(RavenId<Employee> id)
        {
            return await _mediator.Send(new Details.Query { Id = id });
        }
    }
}