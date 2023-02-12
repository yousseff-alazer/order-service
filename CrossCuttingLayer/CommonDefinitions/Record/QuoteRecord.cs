namespace Connect4Sports.Order.API.CommonDefinitions.Record
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class PlayerQuoteRecord
    {
        public int allocatedSessions;
        public int availableSessions;
        public DateTime createdAt;
        public DateTime deletedAt;
        public int id;
        public string playerId;
        public int remainingSessions;
        public int totalSessions;
        public DateTime updatedAt;
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public string ProviderName { get; set; }
    }

    public class QuoteRecord
    {
        public PlayerQuoteRecord player_quoteRecord;
        public bool isDesc;
        public string orderByColumn;
        public int pageSize;
        public int pageIndex;
        public int languageId;
        public string baseUrl;
    }


}
