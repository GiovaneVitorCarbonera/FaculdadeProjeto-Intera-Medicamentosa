using Interação_Medicamentosa.Context;
using Interação_Medicamentosa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Interação_Medicamentosa.Controllers
{
    [ApiController]
    [Route("interações")]
    public class InteraçãoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public InteraçãoController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<InteraçãoModel>>> GetAll()
        {
            if (_context.Pacientes == null)
                return NotFound();

            return await _context.Interações
                                    .Include(i => i.Medicamento_1)
                                    .Include(i => i.Medicamento_2)
                                    .ToListAsync();
        }


        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult<InteraçãoModel>> Get(int id)
        {
            var i = await _context.Interações.FindAsync(id);
            return i == null ? NotFound() : Ok(i);
        }

        [HttpPost]
        public async Task<ActionResult<InteraçãoModel>> Post(InteraçãoModel data)
        {
            _context.Interações.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = data.Id }, data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<InteraçãoModel>> Put(int id, InteraçãoModel data)
        {
            if (id != data.Id)
                return BadRequest();

            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Interações.Any(i => i.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Interações.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            _context.Interações.Remove(data);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
