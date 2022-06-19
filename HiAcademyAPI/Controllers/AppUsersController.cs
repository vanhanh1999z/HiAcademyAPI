using HiAcademyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HiAcademyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly HIACADEMYContext _context;

        public AppUsersController(HIACADEMYContext context)
        {
            _context = context;
        }

        // GET: api/AppUsers
        [HttpGet]
        public async Task<IActionResult> GetAppUsers()
        {
            if (_context.AppUsers == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                });
            }
            var result = await _context.AppUsers.ToListAsync();
            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = result,
                totalCount = result.Count
            });
        }

        // GET: api/AppUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser(string id)
        {
            if (_context.AppUsers == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                });
            }
            var appUser = await _context.AppUsers.FindAsync(id);

            if (appUser == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                });
            }

            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = appUser,
                totalCount = 1
            });
        }

        // PUT: api/AppUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(string id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(appUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
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

        // POST: api/AppUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        {
            if (_context.AppUsers == null)
            {
                return Problem("Entity set 'HIACADEMYContext.AppUsers'  is null.");
            }
            _context.AppUsers.Add(appUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppUserExists(appUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
        }

        // DELETE: api/AppUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser(string id)
        {
            if (_context.AppUsers == null)
            {
                return NotFound();
            }
            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _context.AppUsers.Remove(appUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppUserExists(string id)
        {
            return (_context.AppUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
