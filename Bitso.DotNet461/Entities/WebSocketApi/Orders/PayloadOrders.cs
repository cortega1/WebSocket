namespace Bitso.Entities.WebSocketApi.Orders
{
    public class PayloadOrders
    {
        public BidsAndAsksPayloads bids { get; set; }
        public BidsAndAsksPayloads asks { get; set; }
    }
}