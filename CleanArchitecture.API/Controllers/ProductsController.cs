using CleanArchitecture.API.Utilities;
using CleanArchitecture.API.Utilities.Filters;
using CleanArchitecture.Application.CQRS.ProductFiles.Commands;
using CleanArchitecture.Application.CQRS.ProductFiles.Queries;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Dtos.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<ProductCreateDto> _validator;

        public ProductsController(IMediator mediator, IValidator<ProductCreateDto> validator)
        {
            _mediator = mediator;
            _validator = validator;
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
            var result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var command = new CreateProductCommand(model, User.Identity.GetUserId());
            var handlerResponse = await _mediator.Send(command);

            if (handlerResponse.Status)
                return Ok(handlerResponse.Data);

            return BadRequest(handlerResponse.Message);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductDisplayDto>> Put(ProductCreateDto model)
        {
            var result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

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
