// <copyright file="AddOrderBookCommand.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using MediatR;
using OrderBook.DAL.Model;
using OrderBook.DTOs;
using OrderBook.HttpClients;

namespace OrderBook.Handlers
{
    public class AddOrderBookCommand : IRequest<Result<DAL.Model.OrderBook>>
    {
        public string? CurrencyPair { get; set; } = "btceur";

        public class Handler : IRequestHandler<AddOrderBookCommand, Result<DAL.Model.OrderBook>>
        {
            private readonly IBitstampClient _bitstampClient;
            private readonly OrderBookContext _orderBookContext;

            public Handler(OrderBookContext orderBookContext, IBitstampClient bitstampClient)
            {
                _orderBookContext = orderBookContext;
                _bitstampClient = bitstampClient;
            }

            public async Task<Result<DAL.Model.OrderBook>> Handle(AddOrderBookCommand request, CancellationToken cancellationToken)
            {
                Result<BitstampOrderBookDto> result = await _bitstampClient.GetOrderBook(request.CurrencyPair!);

                if (result.IsFailure)
                {
                    return Result<DAL.Model.OrderBook>.Failure(result.ErrorMessage, result.ErrorCode);
                }

                var orderBook = new DAL.Model.OrderBook
                {
                    Asks = result.Data!.Asks.Select(ask => new Order(ask)).ToList(),
                    Bids = result.Data!.Bids.Select(bid => new Order(bid)).ToList(),
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(result.Data.Timestamp)).DateTime.ToUniversalTime(),
                    CurrencyPair = request.CurrencyPair!,
                };

                await _orderBookContext.AddAsync(orderBook, cancellationToken);
                await _orderBookContext.SaveChangesAsync(cancellationToken);

                return Result.Success(orderBook);
            }
        }
    }
}
