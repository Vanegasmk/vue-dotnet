using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareaController  : ControllerBase
    {
        private readonly DataBaseContext _context;

        public TareaController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> Get()
        {
            try
            {
                var listTareas =  await _context.Tareas.ToListAsync();
                return Ok(listTareas);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Tarea>> Post([FromBody]Tarea tarea)
        {
            try
            {
                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Post), new {id = tarea.Id},tarea);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tarea>> Put(int id, [FromBody] Tarea tarea)
        {
            try
            {
                if(id != tarea.Id)
                {
                    return NotFound();
                }
                
                tarea.Estado = !tarea.Estado;
                _context.Entry(tarea).State =  EntityState.Modified;
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Put), new {id = tarea.Id},tarea);

            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tarea>> Delete(int id)
        {
            try
            {
                var tarea = await _context.Tareas.FindAsync(id);

                if (tarea == null)
                {
                    return NotFound();
                }

                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Delete), new {id = tarea.Id},tarea);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}