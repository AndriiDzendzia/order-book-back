namespace OrderBook.DTOs
{
    public class AuditDto
    {
        public IEnumerable<DateTime> Timestamps { get; set; } = null!;

        public string CurrencyPair { get; set; } = null!;
    }
}
