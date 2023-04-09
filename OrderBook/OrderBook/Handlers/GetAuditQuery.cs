// <copyright file="GetAuditQuery.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderBook.DAL.Model;
using OrderBook.DTOs;

namespace OrderBook.Handlers
{
    public class GetAuditQuery : IRequest<Result<IDictionary<string, IEnumerable<DateTime>>>>
    {
        public class Handler : IRequestHandler<GetAuditQuery, Result<IDictionary<string,IEnumerable<DateTime>>>>
        {
            private readonly OrderBookContext _orderBookContext;

            public Handler(OrderBookContext orderBookContext)
            {
                _orderBookContext = orderBookContext;
            }

            public async Task<Result<IDictionary<string, IEnumerable<DateTime>>>> Handle(GetAuditQuery request, CancellationToken cancellationToken)
            {
                IDictionary<string, IEnumerable<DateTime>> result = await _orderBookContext.OrderBooks
                    .GroupBy(o => o.CurrencyPair)
                    .ToDictionaryAsync(pair => pair.Key, pair => pair.Select(o => o.Timestamp), cancellationToken);

                return Result.Success(result);
            }
        }
    }
}
