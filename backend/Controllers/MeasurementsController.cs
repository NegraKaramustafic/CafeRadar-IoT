using CafeRadar.Api.Data;
using CafeRadar.Api.Dtos;
using CafeRadar.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeRadar.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MeasurementsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeasurement([FromBody] MeasurementCreateDto dto)
        {
            var cafe = await _context.Cafes.FindAsync(dto.CafeId);
            if (cafe == null)
                return BadRequest($"Cafe with ID {dto.CafeId} does not exist.");

            var measurement = new Measurement
            {
                CafeId = dto.CafeId,
                NoiseValue = dto.NoiseValue,
                NoiseLevel = dto.NoiseLevel,
                LightValue = dto.LightValue,
                LightLevel = dto.LightLevel,
                MeasuredAtUtc = DateTime.Now
            };

            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();

            return Ok(measurement);
        }

        [HttpGet("latest/{cafeId}")]
        public IActionResult GetLatest(int cafeId)
        {
            var latest = _context.Measurements
                .Where(m => m.CafeId == cafeId)
                .OrderByDescending(m => m.MeasuredAtUtc)
                .FirstOrDefault();

            if (latest == null)
                return NotFound("No measurements found for this cafe.");

            return Ok(latest);
        }
    }
}
    

