// <copyright file="OrderBookController.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderBook.DTOs;
using OrderBook.Handlers;

namespace OrderBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderBookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderBookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<OrderBookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<OrderBookDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] GetOrderBookQuery query)
        {
            Result<OrderBookDto> result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
