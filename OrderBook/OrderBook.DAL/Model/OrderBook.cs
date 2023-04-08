// <copyright file="OrderBook.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OrderBook.DAL.Model
{
    public class OrderBook
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string CurrencyPair { get; set; } = string.Empty;

        [Column(TypeName = "jsonb")]
        public List<Order> Bids { get; set; } = new List<Order>();

        [Column(TypeName = "jsonb")]
        public List<Order> Asks { get; set; } = new List<Order>();
    }
}
