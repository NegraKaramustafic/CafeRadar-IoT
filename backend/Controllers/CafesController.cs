using CafeRadar.Api.Data;
using CafeRadar.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeRadar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CafesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CafesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<CafeStatusDto>>> GetStatus([FromQuery] string? noiseLevel)
        {
            var query = _context.Cafes
                .Include(c => c.Measurements)
                .AsNoTracking()
                .Select(c => new CafeStatusDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,

                    NoiseLevel = c.Measurements
                        .OrderByDescending(m => m.MeasuredAtUtc)
                        .Select(m => m.NoiseLevel)
                        .FirstOrDefault(),

                    LightLevel = c.Measurements
                        .OrderByDescending(m => m.MeasuredAtUtc)
                        .Select(m => m.LightLevel)
                        .FirstOrDefault(),

                    MeasuredAtUtc = c.Measurements
                        .OrderByDescending(m => m.MeasuredAtUtc)
                        .Select(m => (DateTime?)m.MeasuredAtUtc)
                        .FirstOrDefault()
                });

            if (!string.IsNullOrWhiteSpace(noiseLevel))
            {
                query = query.Where(c => c.NoiseLevel == noiseLevel);
            }

            var result = await query.ToListAsync();
            return Ok(result);
        }
    }
}
