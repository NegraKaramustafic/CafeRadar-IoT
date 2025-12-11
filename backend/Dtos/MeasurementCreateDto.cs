namespace CafeRadar.Api.Dtos
{
    public class MeasurementCreateDto
    {
        public int CafeId { get; set; }

        public int NoiseValue { get; set; }
        public string NoiseLevel { get; set; } = default!;

        public int LightValue { get; set; }
        public string LightLevel { get; set; } = default!;
    }
}
