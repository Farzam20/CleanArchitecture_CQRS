using CleanArchitecture.API.Utilities;
using CleanArchitecture.API.Utilities.Filters;
using CleanArchitecture.Application.CQRS.ProductFiles.Commands;
using CleanArchitecture.Application.CQRS.ProductFiles.Queries;
using CleanArchitecture.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductDisplayDto>>> Get(string? createdByUserId)
        {
            var query = new GetAllProductsQuery(createdByUserId);
            var handlerResponse = await _mediator.Send(query);

            if (handlerResponse.Status)
                return Ok(handlerResponse.Data);

            return BadRequest(handlerResponse.Message);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ProductDisplayDto>> Get(int id)
        {
            var query = new GetProductQuery(id);
            var handlerResponse = await _mediator.Send(query);

            if (handlerResponse.Status)
                return Ok(handlerResponse.Data);

            return BadRequest(handlerResponse.Message);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDisplayDto>> Post(ProductCreateDto model)
        {
            var command = new CreateProductCommand(model);
            var handlerResponse = await _mediator.Send(command);

            if (handlerResponse.Status)
                return Ok(handlerResponse.Data);

            return BadRequest(handlerResponse.Message);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductDisplayDto>> Put(ProductCreateDto model)
        {
            var command = new UpdateProductCommand(model, User.Identity.GetUserId());
            var handlerResponse = await _mediator.Send(command);

            if (handlerResponse.Status)
                return Ok(handlerResponse.Data);

            return BadRequest(handlerResponse.Message);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand(id, User.Identity.GetUserId());
            var handlerResponse = await _mediator.Send(command);

            if (handlerResponse.Status)
                return Ok();

            return BadRequest(handlerResponse.Message);
        }
    }
}
