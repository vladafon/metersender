using Newtonsoft.Json;
using System;
namespace MetersSender.Saures.Models.Requests
{
    internal class UserObjectsRequest
    {
        [JsonProperty("objects")]
        public List<SauresObject> Objects { get; set; }
    }
}
