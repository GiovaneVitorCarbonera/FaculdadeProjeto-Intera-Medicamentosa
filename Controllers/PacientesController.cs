using Interação_Medicamentosa.Context;
using Interação_Medicamentosa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interação_Medicamentosa.Controllers
{
    [Route("pacientes")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PacientesController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PacienteModel>>> GetPatients()
        {
            if (_context.Pacientes == null)
            {
                return NotFound();
            }
            return await _context.Pacientes
                                    .Include(p => p.Prescrições)
                                        .ThenInclude(pr => pr.Medicamento)
                                    .ToListAsync();
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult<PacienteModel>> GetPatient(int id)
        {
            var patient = await _context.Pacientes
                                            .Include(p => p.Prescrições)
                                            .ThenInclude(pr => pr.Medicamento)
                                            .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return NotFound();

            return patient;
        }

        [HttpGet()]
        [Route("procurar/{query}")]
        public async Task<ActionResult<IEnumerable<PacienteModel>>> Search(string query)
        {
            query = query.ToLower();

            var result = await _context.Pacientes
                .Where(p =>
                    p.Nome.ToLower().Contains(query) ||
                    p.CPF.Contains(query) ||
                    p.CondicoesMedicas.Any(c => c.ToLower().Contains(query))
                )
                .ToListAsync();

                 if (result.Count == 0)
                 return NotFound();

            return result;
        }

        [HttpPost()]
        public async Task<ActionResult<PacienteModel>> PostPatient(PacienteModel patient)
        {
            _context.Pacientes.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, PacienteModel patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pacientes.Any(p => p.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Pacientes.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
