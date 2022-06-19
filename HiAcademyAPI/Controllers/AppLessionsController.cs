using HiAcademyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HiAcademyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppLessionsController : ControllerBase
    {
        private readonly HIACADEMYContext _context;

        public AppLessionsController(HIACADEMYContext context)
        {
            _context = context;
        }

        // GET: api/AppLessions
        [HttpGet]
        public async Task<IActionResult> GetAppLessions()
        {
            if (_context.AppLessions == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                });
            }
            var result = await _context.AppLessions.ToListAsync();
            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = result,
                totalCount = result.Count
            });
        }

        // GET: api/AppLessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppLession(string id)
        {
            if (_context.AppLessions == null)
            {
                return NotFound();
            }
            var result = await _context.AppLessions.FindAsync(id);

            if (result == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                });
            }

            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = result,
                totalCount = 1
            });
        }

        // PUT: api/AppLessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppLession(string id, AppLession appLession)
        {
            if (id != appLession.Id)
            {
                return BadRequest();
            }

            _context.Entry(appLession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppLessionExists(id))
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

        // POST: api/AppLessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppLession>> PostAppLession(AppLession appLession)
        {
            if (_context.AppLessions == null)
            {
                return Problem("Entity set 'HIACADEMYContext.AppLessions'  is null.");
            }
            _context.AppLessions.Add(appLession);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppLessionExists(appLession.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAppLession", new { id = appLession.Id }, appLession);
        }

        // DELETE: api/AppLessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppLession(string id)
        {
            if (_context.AppLessions == null)
            {
                return NotFound();
            }
            var appLession = await _context.AppLessions.FindAsync(id);
            if (appLession == null)
            {
                return NotFound();
            }

            _context.AppLessions.Remove(appLession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppLessionExists(string id)
        {
            return (_context.AppLessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
