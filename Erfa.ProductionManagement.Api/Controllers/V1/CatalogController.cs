using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erfa.ProductionManagement.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("All", Name = "All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetAllItems()
        {
            var result = "Hello";
            return Ok(result);
        }

        [HttpPost("Create", Name = "CreateNewProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Create([FromBody] CreateProductRequestModel request, [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new CreateProductCommand(request, userName));
            return StatusCode(201, result);
        }
    }
}

