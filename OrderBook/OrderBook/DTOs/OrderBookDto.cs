// <copyright file="OrderBookDto.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using OrderBook.DAL.Model;

namespace OrderBook.DTOs
{
    public class OrderBookDto
    {
        public DateTime Timestamp { get; set; }

        public string CurrencyPair { get; set; } = null!;

        public IEnumerable<Order> Bids { get; set; } = null!;

        public IEnumerable<Order> Asks { get; set; } = null!;
    }
}
