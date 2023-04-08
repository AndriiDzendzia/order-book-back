// <copyright file="GetAuditQuery.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderBook.DAL.Model;
using OrderBook.DTOs;

namespace OrderBook.Handlers
{
    public class GetAuditQuery : IRequest<Result<IEnumerable<AuditDto>>>
    {
        public class Handler : IRequestHandler<GetAuditQuery, Result<IEnumerable<AuditDto>>>
        {
            private readonly OrderBookContext _orderBookContext;

            public Handler(OrderBookContext orderBookContext)
            {
                _orderBookContext = orderBookContext;
            }

            public async Task<Result<IEnumerable<AuditDto>>> Handle(GetAuditQuery request, CancellationToken cancellationToken)
            {
                List<AuditDto> result = await _orderBookContext.OrderBooks
                    .GroupBy(o => o.CurrencyPair)
                    .Select(pair =>
                        new AuditDto
                        {
                            CurrencyPair = pair.Key,
                            Timestamps = pair.Select(o => o.Timestamp),
                        })
                    .ToListAsync(cancellationToken);

                return Result.Success(result.AsEnumerable());
            }
        }
    }
}
