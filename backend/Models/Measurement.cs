using System.Text.Json.Serialization;

namespace CafeRadar.Api.Models
{
    public class Measurement
    {
        public int Id {  get; set; }
        public int CafeId {  get; set; }

        [JsonIgnore]
        public Cafe Cafe { get; set; } = default!;

        public int NoiseValue { get; set; }
        public string NoiseLevel { get; set; } = default!;

        public int LightValue { get; set; }
        public string LightLevel { get; set; } = default!; 

        public DateTime MeasuredAtUtc { get; set; }
    }
}
