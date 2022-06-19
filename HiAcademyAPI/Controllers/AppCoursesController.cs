using AutoMapper;
using HiAcademyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HiAcademyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppCoursesController : ControllerBase
    {
        private readonly HIACADEMYContext _context;
        private readonly IMapper _mapper;

        public AppCoursesController(HIACADEMYContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/AppCourses
        [HttpGet]
        public async Task<IActionResult> GetAppCourses()
        {
            if (_context.AppCourses == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                });
            }
            var result = await _context.AppCourses.ToListAsync();
            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = result,
                totalCount = result.Count
            });
        }

        // GET: api/AppCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppCourse>> GetAppCourse(string id)
        {
            if (_context.AppCourses == null)
            {
                return NotFound();
            }
            var result = await _context.AppCourses.FindAsync(id);

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

        // PUT: api/AppCourses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppCourse(string id, AppCourse appCourse)
        {
            if (id != appCourse.Id)
            {
                return BadRequest();
            }

            _context.Entry(appCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppCourseExists(id))
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

        // POST: api/AppCourses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppCourse>> PostAppCourse(AppCourse appCourse)
        {
            if (_context.AppCourses == null)
            {
                return Problem("Entity set 'HIACADEMYContext.AppCourses'  is null.");
            }
            _context.AppCourses.Add(appCourse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppCourseExists(appCourse.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAppCourse", new { id = appCourse.Id }, appCourse);
        }

        // DELETE: api/AppCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppCourse(string id)
        {
            if (_context.AppCourses == null)
            {
                return NotFound();
            }
            var appCourse = await _context.AppCourses.FindAsync(id);
            if (appCourse == null)
            {
                return NotFound();
            }

            _context.AppCourses.Remove(appCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppCourseExists(string id)
        {
            return (_context.AppCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
