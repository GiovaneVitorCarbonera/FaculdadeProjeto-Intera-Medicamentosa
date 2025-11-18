using Interação_Medicamentosa.Context;
using Interação_Medicamentosa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interação_Medicamentosa.Controllers
{
    [ApiController]
    [Route("prescrições")]
    public class PrescriçãoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PrescriçãoController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriçãoModel>>> GetPrescriçãoModel()
        {
            if (_context.Prescrições == null)
            {
                return NotFound();
            }
            return await _context.Prescrições
                                    .Include(pr => pr.Medicamento)
                                    .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PrescriçãoModel>> PostPrescriçãoModel([FromBody] PrescriçãoModel PrescriçãoObj)
        {
            if (!_context.Pacientes.Any(x => x.Id == PrescriçãoObj.IdPaciente))
                return NotFound("Not Exist Paciente");
            if (!_context.Medicamentos.Any(x => x.Id == PrescriçãoObj.IdMedicamento))
                return NotFound("Not Exist Medicamento");

            _context.Prescrições.Add(PrescriçãoObj);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPrescriçãoModel", new { id = PrescriçãoObj.Id }, PrescriçãoObj);
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult<PrescriçãoModel>> GetPrescriçãoModel(int id)
        {
            if (_context == null)
            {
                return Problem();
            }
            var PrescriçãoModel = await _context.Prescrições
                                    .Include(pr => pr.Medicamento)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (PrescriçãoModel == null)
            {
                return NotFound("Id: " + id + " não encontrada no banco de dados");
            }
            return PrescriçãoModel;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPrescriçãoModel(int id,[FromBody] PrescriçãoModel PrescriçãoObj)
        {
            if (id != PrescriçãoObj.Id)
            {
                return BadRequest();
            }
            _context.Entry(PrescriçãoObj).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriçãoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescriçãoModel(int id)
        {
            if (_context.Prescrições == null)
            {
                return Problem();
            }
            var PrescriçãoModel = await _context.Prescrições.FindAsync(id);
            if (PrescriçãoModel == null)
            {
                return NotFound();
            }

            _context.Prescrições.Remove(PrescriçãoModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PrescriçãoModelExists(int id)
        {
            return (_context.Prescrições?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
