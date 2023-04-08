// <copyright file="GetCurrencyPairQuery.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using MediatR;
using OrderBook.DTOs;

namespace OrderBook.Handlers
{
    public class GetCurrencyPairsQuery : IRequest<Result<IEnumerable<string>>>
    {
        public class Handler : IRequestHandler<GetCurrencyPairsQuery, Result<IEnumerable<string>>>
        {
            private readonly IConfiguration _configuration;

            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<Result<IEnumerable<string>>> Handle(GetCurrencyPairsQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(
                    Result.Success(
                        _configuration.GetSection("CurrencyPairs").Get<IEnumerable<string>>()!));
            }
        }
    }
}