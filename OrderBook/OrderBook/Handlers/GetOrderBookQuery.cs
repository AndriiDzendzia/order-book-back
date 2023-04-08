// <copyright file="GetOrderBookQuery.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using MediatR;
using OrderBook.DTOs;
using OrderBook.Extensions;

namespace OrderBook.Handlers
{
    public class GetOrderBookQuery : IRequest<Result<OrderBookDto>>
    {
        public DateTime? Timestamp { get; set; }

        public string? CurrencyPair { get; set; } = "btceur";

        public class Handler : IRequestHandler<GetOrderBookQuery, Result<OrderBookDto>>
        {
            private readonly IMediator _mediator;

            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<Result<OrderBookDto>> Handle(GetOrderBookQuery request, CancellationToken cancellationToken)
            {
                Result<DAL.Model.OrderBook>? orderBookResult = null;
                if (request.Timestamp.HasValue)
                {
                    orderBookResult = await _mediator.Send(
                        new GetOrderBookFromDBQuery
                        {
                            CurrencyPair = request.CurrencyPair,
                            Timestamp = request.Timestamp.Value,
                        },
                        cancellationToken);
                }
                else
                {
                    orderBookResult = await _mediator.Send(
                        new AddOrderBookCommand { CurrencyPair = request.CurrencyPair },
                        cancellationToken);
                }

                if (orderBookResult.IsFailure)
                {
                    return Result<OrderBookDto>.Failure(orderBookResult.ErrorMessage, orderBookResult.ErrorMessage);
                }

                return Result.Success(orderBookResult.Data!.ToDto());
            }
        }
    }
}
