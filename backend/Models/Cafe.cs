namespace CafeRadar.Api.Models
{
    public class Cafe
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
    }
}
