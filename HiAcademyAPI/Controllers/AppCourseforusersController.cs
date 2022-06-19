using AutoMapper;
using Dapper;
using HiAcademyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace HiAcademyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppCourseforusersController : ControllerBase
    {
        private readonly HIACADEMYContext _context;
        private readonly IMapper _mapper;
        public AppCourseforusersController(HIACADEMYContext context, IMapper mapper)
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
                    var query = @"SELECT        dbo.APP_COURSEFORUSER.IDUSER, dbo.APP_USER.USERNAME, dbo.APP_USER.PASSWORD, dbo.APP_COURSEFORUSER.IDCOURSE, dbo.APP_COURSE.NAME, dbo.APP_COURSE.DESCRIPTION, 
                         dbo.APP_COURSEFORUSER.STATUS
                        FROM            dbo.APP_COURSEFORUSER INNER JOIN
                         dbo.APP_USER ON dbo.APP_COURSEFORUSER.IDUSER = dbo.APP_USER.ID INNER JOIN
                         dbo.APP_COURSE ON dbo.APP_COURSEFORUSER.IDCOURSE = dbo.APP_COURSE.ID
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
    }
}
