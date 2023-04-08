// <copyright file="OrderBookContext.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;

namespace OrderBook.DAL.Model
{
    public class OrderBookContext : DbContext
    {
        public OrderBookContext(DbContextOptions<OrderBookContext> options)
            : base(options)
        { }

        public DbSet<OrderBook> OrderBooks { get; set; }
    }
}
