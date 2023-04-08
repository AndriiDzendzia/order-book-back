using OrderBook.DTOs;

namespace OrderBook.HttpClients
{
    public class BitstampClient : IBitstampClient
    {
        private const string RequestUri = "https://www.bitstamp.net/api/v2/order_book/";

        private readonly HttpClient _httpClient;
        private readonly ILogger<BitstampClient> _logger;

        public BitstampClient(HttpClient httpClient, ILogger<BitstampClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Result<BitstampOrderBookDto>> GetOrderBook(string currencyPair)
        {
            HttpResponseMessage responce = await _httpClient.GetAsync($"{RequestUri}{currencyPair}/");

            if (responce.IsSuccessStatusCode)
            {
                BitstampOrderBookDto? orderBook = await responce.Content.ReadFromJsonAsync<BitstampOrderBookDto>();

                if (orderBook is not null)
                {
                    return Result.Success(orderBook);
                }
            }
            else
            {
                _logger.LogError($"Bitstamp error: {await responce.Content.ReadAsStringAsync()}");
            }

            return Result<BitstampOrderBookDto>.Failure("Bitstamp responded with error");
        }
    }
}
