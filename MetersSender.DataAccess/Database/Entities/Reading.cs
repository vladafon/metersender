namespace MetersSender.DataAccess.Database.Entities
{
    public class Reading
    {
        public long ReadingId { get; set; }

        public long RequestId { get; set; }

        public Request Request { get; set; }

        public long MeterId { get; set; }

        public Meter Meter { get; set; }

        public decimal Value { get; set; }
    }
}
