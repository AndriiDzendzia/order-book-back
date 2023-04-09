// <copyright file="TestBase.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using AutoFixture;

namespace OrderBook.Test
{
    public class TestBase
    {
        public TestBase()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        protected Fixture Fixture { get; set; }
    }
}
