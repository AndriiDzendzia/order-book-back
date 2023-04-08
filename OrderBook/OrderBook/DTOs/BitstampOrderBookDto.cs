// <copyright file="BitstampOrderBookDto.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

namespace OrderBook.DTOs
{
    public class BitstampOrderBookDto
    {
        public IEnumerable<IEnumerable<string>> Asks { get; set; } = null!;

        public IEnumerable<IEnumerable<string>> Bids { get; set; } = null!;
    }
}
