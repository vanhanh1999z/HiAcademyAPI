using AutoMapper;
using Dapper;
using HiAcademyAPI.ModelDTO;
using HiAcademyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace HiAcademyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppCourseinfoController : ControllerBase
    {
        private readonly HIACADEMYContext _context;
        private readonly IMapper _mapper;

        public AppCourseinfoController(HIACADEMYContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetListAll()
        {
            using (var connection = new SqlConnection(new ConnectDB().conn))
            {
                try
                {
                    var query = @"SELECT        dbo.APP_COURSEINFO.IDCOURSE AS idcourse, dbo.APP_COURSE.NAME AS name, dbo.APP_COURSE.DESCRIPTION AS description, dbo.APP_COURSEINFO.IDLESSION AS idlession, dbo.APP_LESSION.NAME AS namelession,
                         dbo.APP_LESSION.DESCRIPTION AS deslession, dbo.APP_LESSION.IMAGE AS image, dbo.APP_LESSION.SOUND AS sound
FROM            dbo.APP_COURSEINFO INNER JOIN
                         dbo.APP_COURSE ON dbo.APP_COURSEINFO.IDCOURSE = dbo.APP_COURSE.ID INNER JOIN
                         dbo.APP_LESSION ON dbo.APP_COURSEINFO.IDLESSION = dbo.APP_LESSION.ID
                                ";
                    var res = await connection.QueryAsync(query);
                    return Ok(new ResultMessageResponse()
                    {
                        success = true,
                        data = res,
                        totalCount = res.Count(),
                    });
                }
                catch (Exception)
                {
                    return Ok(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Error"
                    });
                }
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetListById(string id)
        {
            using (var connection = new SqlConnection(new ConnectDB().conn))
            {
                try
                {
                    var query = $@"SELECT        dbo.APP_COURSEINFO.IDCOURSE AS idcourse, dbo.APP_COURSE.NAME AS name, dbo.APP_COURSE.DESCRIPTION AS description, dbo.APP_COURSEINFO.IDLESSION AS idlession, dbo.APP_LESSION.NAME AS namelession,
                         dbo.APP_LESSION.DESCRIPTION AS deslession, dbo.APP_LESSION.IMAGE AS image, dbo.APP_LESSION.SOUND AS sound
FROM            dbo.APP_COURSEINFO INNER JOIN
                         dbo.APP_COURSE ON dbo.APP_COURSEINFO.IDCOURSE = dbo.APP_COURSE.ID INNER JOIN
                         dbo.APP_LESSION ON dbo.APP_COURSEINFO.IDLESSION = dbo.APP_LESSION.ID

                        WHERE IDCOURSE = '{id}' OR IDLESSION = '{id}'
                                ";
                    var res = await connection.QueryAsync(query);
                    return Ok(new ResultMessageResponse()
                    {
                        success = true,
                        data = res,
                        totalCount = res.Count(),
                    });
                }
                catch (Exception)
                {
                    return Ok(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Error"
                    });
                }
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] AppCourseinfoDTO model)
        {
            using (var connection = new SqlConnection(new ConnectDB().conn))
            {
                var Course = await _context.AppCourses.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(model.Idcourse));
                var Lession = await _context.AppLessions.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(model.Idlession));
                if (Course == null || Lession == null)
                {
                    return Ok(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Không tồn tại khóa học hoặc bài học trên, vui lòng kiểm tra lại!"
                    });
                }
                try
                {
                    var query = $@"SELECT        dbo.APP_COURSEINFO.IDCOURSE, dbo.APP_COURSE.NAME, dbo.APP_COURSE.DESCRIPTION, dbo.APP_COURSEINFO.IDLESSION, dbo.APP_LESSION.NAME AS NAMELESSION, dbo.APP_LESSION.DESCRIPTION AS NAMEDES,
                          dbo.APP_LESSION.IMAGE, dbo.APP_LESSION.SOUND
                            FROM            dbo.APP_COURSEINFO INNER JOIN
                         dbo.APP_COURSE ON dbo.APP_COURSEINFO.IDCOURSE = dbo.APP_COURSE.ID INNER JOIN
                         dbo.APP_LESSION ON dbo.APP_COURSEINFO.IDLESSION = dbo.APP_LESSION.ID

                        WHERE IDCOURSE = '{model.Idcourse}' AND IDLESSION = '{model.Idlession}'
                                ";
                    var res = await connection.QueryAsync(query);
                    if (res.Count() > 0)
                    {
                        return Ok(new ResultMessageResponse()
                        {
                            success = false,
                            message = "Đã tồn tại phiên bài học này, vui lòng kiểm tra lại!"
                        });
                    }

                    var result = _mapper.Map<AppCourseinfo>(model);

                    await _context.AddAsync(result);
                    await _context.SaveChangesAsync();
                    return Ok(new ResultMessageResponse()
                    {
                        success = true,
                        data = result,
                        message = "Thành công!"
                    });
                }
                catch (Exception)
                {
                    return Ok(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Error"
                    });
                }
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> Upadte([FromBody] AppCourseinfoDTO model)
        {
            var Course = await _context.AppCourseinfos.AsNoTracking().FirstOrDefaultAsync(x => x.Idcourse.Equals(model.Idcourse) && x.Idlession.Equals(model.Idlession));
            if (Course == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Không tồn tại khóa học vui lòng kiểm tra lại!"
                });
            }
            _context.AppCourseinfos.Remove(Course);
            await _context.SaveChangesAsync();
            var res = new ResultMessageResponse()
            {
                success = true,
                message = "Thành công",
            };
            return Ok(res);
        }
    }
}