namespace CafeRadar.Api.Dtos
{
    public class CafeStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        public string? NoiseLevel { get; set; } = default!;
        public string? LightLevel { get; set; } = default!;

        public DateTime? MeasuredAtUtc { get; set; }
    }
}
