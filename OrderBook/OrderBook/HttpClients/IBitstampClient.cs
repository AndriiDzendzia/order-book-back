// <copyright file="IBitstampClient.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using OrderBook.DTOs;

namespace OrderBook.HttpClients
{
    public interface IBitstampClient
    {
        Task<Result<BitstampOrderBookDto>> GetOrderBook(string currencyPair);
    }
}
