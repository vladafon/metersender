namespace MetersSender.DataAccess.Database.Entities
{
    public class House
    {
        public long HouseId { get; set; }

        public string Name { get; set; }

        public long SourceServiceId { get; set; }

        public Service SourceService { get; set; }

        public long RecepientServiceId { get; set; }

        public Service RecepientService { get; set; }

        public ICollection<Meter> Meters { get; set; }
    }
}
