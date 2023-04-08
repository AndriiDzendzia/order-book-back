// <copyright file="Order.cs" company="AndriiDzendzia">
// Copyright (c) AndriiDzendzia. All rights reserved.
// </copyright>

using System.Globalization;

namespace OrderBook.DAL.Model
{
    public class Order
    {
        public Order()
        { }

        public Order(IEnumerable<string> values)
        {
            Price = Convert.ToDouble(values.ElementAt(0), CultureInfo.InvariantCulture);
            Amount = Convert.ToDouble(values.ElementAt(1), CultureInfo.InvariantCulture);
        }

        public double Price { get; set; }

        public double Amount { get; set; }
    }
}
