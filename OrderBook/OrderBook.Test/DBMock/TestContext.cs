// <copyright file="TestContext.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using OrderBook.DAL.Model;
using System.Text.Json;

namespace OrderBook.Test.DBMock
{
    internal class TestContext : OrderBookContext
    {
        public TestContext(DbContextOptions<OrderBookContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=:memory:");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DAL.Model.OrderBook>(entity =>
            {
                entity
                    .Property(e => e.Asks)
                    .HasConversion(
                        a => JsonSerializer.Serialize(a, JsonSerializerOptions.Default),
                        a => JsonSerializer.Deserialize<IEnumerable<Order>>(a, JsonSerializerOptions.Default)!);

                entity
                    .Property(e => e.Bids)
                    .HasConversion(
                        b => JsonSerializer.Serialize(b, JsonSerializerOptions.Default),
                        b => JsonSerializer.Deserialize<IEnumerable<Order>>(b, JsonSerializerOptions.Default)!);
            });
        }
    }
}
