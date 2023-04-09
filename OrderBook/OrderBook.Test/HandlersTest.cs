// <copyright file="HandlersTest.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderBook.DAL.Model;
using OrderBook.DTOs;
using OrderBook.Handlers;
using OrderBook.HttpClients;
using OrderBook.Test.DBMock;

namespace OrderBook.Test
{
    public class HandlersTest : TestBase
    {
        private readonly OrderBookContext _dbContext;
        private readonly Mock<IBitstampClient> _bitstampClient;

        public HandlersTest()
        {
            _dbContext = new TestContext(new DbContextOptions<OrderBookContext>());
            _dbContext.Database.OpenConnection();
            _dbContext.Database.EnsureCreated();

            _bitstampClient = new Mock<IBitstampClient>();
            _bitstampClient
                .Setup(client => client.GetOrderBook(It.IsAny<string>()))
                .ReturnsAsync(
                    Result.Success(
                        new BitstampOrderBookDto
                        {
                            Asks = new List<List<string>> { new List<string> { "12.12", "1.12" }, new List<string> { "22.13", "5.12" } },
                            Bids = new List<List<string>> { new List<string> { "42.12", "8.12" }, new List<string> { "72.12", "7.12" }, },
                        }));
        }

        [Fact]
        public async Task AddOrderBook_EntityIsValid_EntityIsAdded()
        {
            // Arrange
            var command = new AddOrderBookCommand();
            var handler = new AddOrderBookCommand.Handler(_dbContext, _bitstampClient.Object);

            // Act
            Result<DAL.Model.OrderBook> result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _dbContext.Entry(result.Data!).State.Should().Be(EntityState.Unchanged);
        }

        [Fact]
        public async Task GetOrderBook_EntityExist_ReturnsEntity()
        {
            // Arrange
            DAL.Model.OrderBook data = Fixture
                .Build<DAL.Model.OrderBook>()
                .With(o => o.Id, 0)
                .Create();

            await _dbContext.AddAsync(data);
            await _dbContext.SaveChangesAsync();

            var query = new GetOrderBookFromDBQuery { CurrencyPair = data.CurrencyPair, Timestamp = data.Timestamp };
            var handler = new GetOrderBookFromDBQuery.Handler(_dbContext);

            // Act
            Result<DAL.Model.OrderBook> result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data!.Should().BeEquivalentTo(data);
        }

        [Fact]
        public async Task GetAudit_EntitiesExist_ReturnsEntities()
        {
            // Arrange
            IEnumerable<DAL.Model.OrderBook> data = Fixture
                .Build<DAL.Model.OrderBook>()
                .With(o => o.Id, 0)
                .CreateMany(50);
            var resultData = data.GroupBy(o => o.CurrencyPair).ToDictionary(pair => pair.Key, pair => pair.Select(o => o.Timestamp));

            await _dbContext.AddRangeAsync(data);
            await _dbContext.SaveChangesAsync();

            var query = new GetAuditQuery();
            var handler = new GetAuditQuery.Handler(_dbContext);

            // Act
            Result<IDictionary<string, IEnumerable<DateTime>>> result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data!.Should().BeEquivalentTo(resultData);
        }
    }
}