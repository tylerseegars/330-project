using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CourseRegistration.Models;
using CourseRegistration.Services;

namespace CourseRegistration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private ICourseServices _courseServices;
        
        public CoursesController(ICourseServices courseServices)
        {
          _courseServices = courseServices;
        }

        [HttpGet]
        public IActionResult getCourses()
        {
            try
            {
                IEnumerable<Course> list = _courseServices.getCourses();
                if(list !=null) return Ok(list);
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{name}")]
        public IActionResult getCourseByName(string name)
        {
            try
            {
                IEnumerable<Course> list = _courseServices.getCourseByName(name);
                if(list !=null) return Ok(list);
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("search")]
        public IActionResult getCourseOfferingsByDept([FromQuery] string dept)
        {
            try
            {
                IEnumerable<CourseOffering> list = _courseServices.getCourseOfferingsByDept(dept);
                if(list !=null) return Ok(list);
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult addCourse([FromBody] Course course)
        {
            if (course == null)
            return BadRequest();
            try
            {
                _courseServices.addCourse(course);

                return CreatedAtAction(nameof(getCourseByName), new { name = course.Name }, course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{name}")]
        public IActionResult updateCourse(string name, [FromBody] Course updatedCourse)
        {
            if (updatedCourse == null)
            return BadRequest();
            try
            {
                var success = _courseServices.updateCourse(name, updatedCourse);

                if (!success)
                return BadRequest();
                else return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{name}")]
        public IActionResult deleteCourse(string name)
        {
            if (name == null)
            return BadRequest();
            try
            {
                _courseServices.deleteCourse(name);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{name}/goals")]
        public IActionResult getGoalsByName(string name)
        {
            if (name == null)
            return BadRequest();
            try
            {
                var goals = _courseServices.getGoalsByCourse(name);

                if (goals == null || goals.Count == 0)
                return NotFound();
                else return Ok(goals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("offerings")]
        public IActionResult getCourseOfferingsBySemester([FromQuery] string semester)
        {
            if (semester == null)
            return BadRequest();
            try
            {
                var offerings = _courseServices.getCourseOfferingsBySemester(semester);

                if (offerings == null || offerings.Count == 0)
                return NotFound();
                else return Ok(offerings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
