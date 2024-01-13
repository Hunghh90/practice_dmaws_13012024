namespace practice_dmaws_hunghhth2203033.Dtos
{
    public class CreateOrder
    {
        public string? ItemName { get; set; }

        public int? ItemQty { get; set; }

        public DateTime? OrderDelivery { get; set; }

        public string? OrderAddress { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
