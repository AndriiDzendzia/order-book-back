// <copyright file="Dto.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using OrderBook.DTOs;

namespace OrderBook.Extensions
{
    public static class Dto
    {
        public static OrderBookDto ToDto(this DAL.Model.OrderBook orderBook) =>
            new ()
            {
                Asks = orderBook.Asks,
                Bids = orderBook.Bids,
                CurrencyPair = orderBook.CurrencyPair,
                Timestamp = orderBook.Timestamp,
            };

        public static DAL.Model.OrderBook ToModel(this OrderBookDto orderBook) =>
            new ()
            {
                Asks = orderBook.Asks,
                Bids = orderBook.Bids,
                CurrencyPair = orderBook.CurrencyPair,
                Timestamp = orderBook.Timestamp,
            };
    }
}
