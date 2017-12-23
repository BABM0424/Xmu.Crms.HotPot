using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;
using Xmu.Crms.Shared.Exceptions;

namespace Xmu.Crms.HotPot.Controllers
{
    [Route("")]
    [Produces("application/json")]
    public class CourseController : Controller
    {
        private readonly ICourseService _service;
        private readonly IClassService _service1;
        private readonly ISeminarService _service2;
        private readonly JwtHead _head;

        public CourseController(ICourseService service,IClassService service1,ISeminarService service2, JwtHead head)
        {
            _service = service;
            _service1 = service1;
            _service2 = service2;
            _head = head;
        }

        
        [HttpGet("/course")]
        public IActionResult GetUserCourses()
        {
            try
            {
                IList<Course> il = _service.ListCourseByUserId(User.Id);
                return Json(il);
            }
            catch (CourseNotFoundException)
            {
                return StatusCode(404, new { msg = "用户所包含课程不存在!" });
            }
        }
        //===
        [HttpPost("/course")]
        public IActionResult CreateCourse([FromBody] Course newCourse)
        {
            _service.InsertCourseByUserId(user.Id, course);
            return Created("/course/{courseId:long}", new {id = course});
        }

        [HttpGet("/course/{courseId:long}")]
        public IActionResult GetCourseById([FromRoute] long courseId)
        {
            try
            {
                cou = _service.GetCourseByCourseId(courseId);
                return Json(cou);
            }
            catch (CourseNotFoundException)
            {
                return StatusCode(404, new { msg = "该课程不存在!" });
            }
        }

        [HttpDelete("/course/{courseId:long}")]
        public IActionResult DeleteCourseById([FromRoute] long courseId)
        {
            _service.DeleteCourseByCourseId(courseId);
            return NoContent();
        }

        [HttpPut("/course/{courseId:long}")]
        public IActionResult UpdateCourseById([FromRoute] long courseId, [FromBody] Course updated)
        {
            _service.UpdateCourseByCourseId(courseId,course);
            return NoContent();
        }

        [HttpGet("/course/{courseId:long}/class")]
        public IActionResult GetClassesByCourseId([FromRoute] string courseName)
        {
            try
            {
                IList<ClassInfo> il = _service1.ListClassByCourseName(courseName);
                return Json(il);
            }
            catch (ClassNotFoundException)
            {
                return StatusCode(404, new { msg = "该课程包含的班级不存在!" });
            }
        }
        /*  在每个Service里都没找到相关调用？是否考虑一下这个返回是否有意义？
        [HttpPost("/course/{courseId:long}/class")]
        public IActionResult CreateClassByCourseId([FromRoute] long courseId, [FromBody] ClassInfo newClass)
        {
            return Created("/class/1", new { id = 1 });
        }
        */
        [HttpGet("/course/{courseId:long}/seminar")]
        public IActionResult GetSeminarsByCourseId([FromRoute] long courseId)
        {
            IList<Seminar> il= _service2.ListSeminarByCourseId(courseId);
            return Json(il);
        }

        /* 同理找不到相关调用。
        [HttpPost("/course/{courseId:long}/seminar")]
        public IActionResult CreateSeminarByCourseId([FromRoute] long courseId, [FromBody] Seminar newSeminar)
        {
            return Created("/seminar/1", new { id = 1 });
        }

        [HttpGet("/course/{courseId:long}/grade")]
        public IActionResult GetGradeByCourseId([FromRoute] long courseId)
        {
            var gd1 = new StudentScoreGroup();
            var gd2 = new StudentScoreGroup();
            return Json(new List<StudentScoreGroup> {gd1, gd2});
        }
        */
    }
}