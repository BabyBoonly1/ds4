using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalculadoraApi.Data;
using CalculadoraApi.Models;


namespace CalculadoraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculosCotroller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CalculosCotroller(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calculo>>> GetCalculos()
        {
            var calculos = await _context.Calculos.ToListAsync();
            return Ok(calculos);
        }
        // GET: api/calculos/sumas
        // Obtener solo las sumas
        [HttpGet("sumas")]
        public async Task<ActionResult<IEnumerable<Calculo>>> GetSumas()
        {
            var sumas = await _context.Calculos
                .Where(c => c.Operacion == "Suma")
                .ToListAsync();
            return Ok(sumas);
        }

        // GET: api/calculos/restas
        // Obtener solo las restas
        [HttpGet("restas")]
        public async Task<ActionResult<IEnumerable<Calculo>>> GetRestas()
        {
            var restas = await _context.Calculos
                .Where(c => c.Operacion == "Resta")
                .ToListAsync();
            return Ok(restas);
        }

        // GET: api/calculos/multiplicaciones
        // Obtener solo las multiplicaciones
        [HttpGet("multiplicaciones")]
        public async Task<ActionResult<IEnumerable<Calculo>>> GetMultiplicaciones()
        {
            var multiplicaciones = await _context.Calculos
                .Where(c => c.Operacion == "Multiplicacion")
                .ToListAsync();
            return Ok(multiplicaciones);
        }

        // GET: api/calculos/divisiones
        // Obtener solo las divisiones
        [HttpGet("divisiones")]
        public async Task<ActionResult<IEnumerable<Calculo>>> GetDivisiones()
        {
            var divisiones = await _context.Calculos
                .Where(c => c.Operacion == "Division")
                .ToListAsync();
            return Ok(divisiones);
        }
    }
}