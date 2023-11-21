namespace MetersSender.DataAccess.Database.Entities
{
    public class Request
    {
        public long RequestId { get; set; }

        public DateTimeOffset SendingDateTimeUtc { get; set; }

        public ICollection<Reading> Readings { get; set; }
    }
}
