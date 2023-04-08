// <copyright file="GetOrderBookFromDBQuery.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderBook.DAL.Model;
using OrderBook.DTOs;

namespace OrderBook.Handlers
{
    public class GetOrderBookFromDBQuery : IRequest<Result<DAL.Model.OrderBook>>
    {
        public DateTime Timestamp { get; set; }

        public string? CurrencyPair { get; set; } = "btceur";

        public class Handler : IRequestHandler<GetOrderBookFromDBQuery, Result<DAL.Model.OrderBook>>
        {
            private readonly OrderBookContext _orderBookContext;

            public Handler(OrderBookContext orderBookContext)
            {
                _orderBookContext = orderBookContext;
            }

            public async Task<Result<DAL.Model.OrderBook>> Handle(GetOrderBookFromDBQuery request, CancellationToken cancellationToken)
            {
                DAL.Model.OrderBook? orderBook = await _orderBookContext.OrderBooks
                    .FirstOrDefaultAsync(
                        o => o.CurrencyPair == request.CurrencyPair && o.Timestamp == request.Timestamp,
                        cancellationToken);

                if (orderBook is null)
                {
                    return Result<DAL.Model.OrderBook>.Failure("No data found");
                }

                return Result.Success(orderBook);
            }
        }
    }
}
