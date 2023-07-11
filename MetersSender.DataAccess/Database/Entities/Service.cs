using System.Collections;

namespace MetersSender.DataAccess.Database.Entities
{
    public class Service
    {
        public long Serviced { get; set; }

        public string Name { get; set; }

        public string ConfigJson { get; set; }

        public ICollection<House> HousesSource { get; set; }
        
        public ICollection<House> HousesRecipient { get; set; }
    }
}
