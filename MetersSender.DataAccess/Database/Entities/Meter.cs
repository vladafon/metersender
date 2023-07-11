namespace MetersSender.DataAccess.Database.Entities
{
    public class Meter
    {
        public long MeterId { get; set; }

        public long HouseId { get; set; }

        public House House { get; set; }

        public string Name { get; set; }

        public string SourceMeterId { get; set; }

        public string RecepientMeterId { get; set; }

        public ICollection<Reading> Readings { get; set; }
    }
}
