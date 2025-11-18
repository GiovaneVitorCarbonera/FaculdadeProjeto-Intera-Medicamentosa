using Interação_Medicamentosa.Context;
using Interação_Medicamentosa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interação_Medicamentosa.Controllers
{
    [ApiController]
    [Route("medicamentos")]
    public class MedicamentoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public MedicamentoController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentoModel>>> Get()
        {
            return await _context.Medicamentos
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicamentoModel>> Get(int id)
        {
            var m = await _context.Medicamentos.FindAsync(id);
            return m == null ? NotFound() : Ok(m);
        }

        [HttpGet]
        [Route("{id}/interacoes")]
        public async Task<ActionResult<IEnumerable<InteraçãoModel>>> Get_Interações(int id)
        {
            var result = await _context.Interações
                .Where(i => i.MedicamentoId_1 == id || i.MedicamentoId_2 == id)
                    .Include(m => m.Medicamento_1)
                    .Include(m => m.Medicamento_2)
                .ToListAsync();

            return result;
        }

        [HttpGet()]
        [Route("search/{query}")]
        public async Task<ActionResult<IEnumerable<MedicamentoModel>>> Search(string query)
        {
            query = query.ToLower();

            var result = await _context.Medicamentos
                .Where(p =>
                    p.Nome.ToLower().Contains(query) ||
                    p.ClasseTerapeutica.ToLower().Contains(query) ||
                    p.FormaFarmaceutica.ToLower().Contains(query) ||
                    p.Fabricante.ToLower().Contains(query)
                )
                .ToListAsync();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<MedicamentoModel>> Post(MedicamentoModel medicamento)
        {
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = medicamento.Id }, medicamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MedicamentoModel medicamento)
        {
            if (id != medicamento.Id) return BadRequest();

            _context.Entry(medicamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _context.Medicamentos.FindAsync(id);
            if (m == null) return NotFound();

            _context.Medicamentos.Remove(m);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
