using Newtonsoft.Json;
using System;
namespace MetersSender.Saures.Models
{
    internal class UserObjectsModel
    {
        [JsonProperty("objects")]
        public List<SauresObject> Objects { get; set; }
    }
}
