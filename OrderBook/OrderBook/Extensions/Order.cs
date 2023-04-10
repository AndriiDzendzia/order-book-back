namespace OrderBook.Extensions
{
    public static class Order
    {
        public static IEnumerable<DAL.Model.Order> Prepare(this IEnumerable<DAL.Model.Order> orders, int limit)
        {
            var amount = 0.0;

            var result = orders
                .Take(limit)
                .OrderBy(o => o.Price)
                .ToList();

            result.ForEach(o => o.Amount = amount += o.Amount);

            return result;
        }
    }
}
