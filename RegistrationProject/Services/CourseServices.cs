using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;
using System.Linq;

namespace CourseRegistration.Services
{
    public class CourseServices : ICourseServices
    {
        private CourseRepository repo = new CourseRepository();


        //As a student, I want to search for course offerings that meet core goals 
        // so that I can register easily for courses that meet my program requirements
         public List<CourseOffering> getOfferingsByGoalIdAndSemester(String theGoalId, String semester) {
          //finish this method during the tutorial 
          //use the repo to get the data from the database (data store)
            List<CoreGoal> theGoals = repo.Goals;
            List<CourseOffering> theOfferings = repo.Offerings;
            
            //Complete any other required functionality/business logic to satisfy the requirement
            CoreGoal theGoal=null;
            foreach(CoreGoal cg in theGoals) {
               if(cg.Id.Equals(theGoalId)) {
                  theGoal=cg; break;
 
               }
            }
            if(theGoal==null) throw new Exception("Didn't find the goal");
            //search list of courses, then for each course, search offerings
            List<CourseOffering> courseOfferingsThatMeetGoal = new List<CourseOffering>();
            
            foreach(CourseOffering c in theOfferings) {
               if(c.Semester.Equals(semester) 
                   && theGoal.Courses.Contains(c.TheCourse) ) 
               {
                   courseOfferingsThatMeetGoal.Add(c);
               }
 
            }
            return courseOfferingsThatMeetGoal;
         }

        
        //Add more service functions here, as needed, for the project
         
        /* As a student, I want to see all available courses so that I know what my options are */
         public List<Course> getCourses() {
            List<Course> allCourses = repo.Courses;

            return allCourses;
         }
         public List<Course> getCourseByName(string name)
         {
            List<Course> allCourses = repo.Courses;
            List<Course> resultCourse = new List<Course>();
            foreach(Course c in allCourses)
            {
               if (c.Name.Equals(name))
               {
                  resultCourse.Add(c);
               }
            }
            if(resultCourse==null)
            {
               throw new Exception("No courses found.");
            }
            return resultCourse;
         }
        /* As a student, I want to see all course offerings by semester, so that I can choose from what's
           available to register for next semester */
         public List<CourseOffering> getCourseOfferingsBySemester(string semester)
      {
         List<CourseOffering> theOfferings = repo.Offerings;
         List<CourseOffering> courseOfferingsBySemester = new List<CourseOffering>();

         foreach(CourseOffering c in theOfferings)
         {
            if (c.Semester.Equals(semester))
            {
               courseOfferingsBySemester.Add(c);
            }
         }
         if (courseOfferingsBySemester==null)
         {
            throw new Exception("No courses found."); 
         }
         return courseOfferingsBySemester;
      }
        /* As a student I want to see all course offerings by semester and department so that I can 
        choose major courses to register for */
         public List<CourseOffering> getCourseOfferingsBySemesterAndDept(String semester, String department)
      {
         List<CourseOffering> theOfferings = repo.Offerings;
         List<CourseOffering> courseOfferingsBySemesterAndDept = new List<CourseOffering>();

         foreach(CourseOffering c in theOfferings)
         {
            if (c.Semester.Equals(semester) && c.TheCourse.Department.Equals(department))
            {
               courseOfferingsBySemesterAndDept.Add(c);
            }
         }
         if (courseOfferingsBySemesterAndDept==null)
         {
            throw new Exception("No Courses Found"); 
         }
         return courseOfferingsBySemesterAndDept;
      }

      public List<CourseOffering> getCourseOfferingsByDept(string department)
      {
         List<CourseOffering> theOfferings = repo.Offerings;
         List<CourseOffering> courseOfferingsByDept = new List<CourseOffering>();

         foreach(CourseOffering c in theOfferings)
         {
            if (c.TheCourse.Department.Equals(department))
            {
               courseOfferingsByDept.Add(c);
            }
         }
         if (courseOfferingsByDept==null)
         {
            throw new Exception("No Courses Found"); 
         }
         return courseOfferingsByDept;
      }

      public void addCourse(Course course)
      {
         if (course == null)
         throw new Exception("Please enter course data.");
         repo.Courses.Add(course);
      }

      public bool updateCourse(string name, Course updatedCourse)
      {
         List<Course> allCourses = repo.Courses;
         foreach(Course c in allCourses)
         {
            if(c.Name.Equals(name))
            {
               c.Title = updatedCourse.Title;
               c.Credits = updatedCourse.Credits;
               c.Description = updatedCourse.Description;
               c.Department = updatedCourse.Department;

               return true;
            }
         }
         return false;
      }

      public bool deleteCourse(string name)
      {
         List<Course> allCourses = repo.Courses;
         foreach(Course c in allCourses)
         {
            if(c.Name.Equals(name))
            {
               repo.Courses.Remove(c);
               return true;
            }
         }
         return false;
      }

      public List<CoreGoal> getGoalsByCourse(string name)
      {
         List<Course> allCourses = repo.Courses;
         foreach(Course c in allCourses)
         {
            if(c.Name.Equals(name))
            {
               var goals = repo.Goals.Where(g => g.Courses.Contains(c)).ToList();
               return goals;
            }
         }
         throw new Exception("No course found.");
      }
        /* As a student I want to see all courses that meet a core goal, so that I can plan out
           my courses over the next few semesters and choose core courses that make sense for me */

        /* As a student I want to find a course that meets two different core goals, so that I can
        "feed two birds with one seed" (save time by taking one class that will fulfill two 
          requirements */

        /* As a freshman adviser, I want to see all the core goals which do not have any course offerings 
           for a given semester, so that I can work with departments to get some courses offered
           that students can take to meet those goals */     
    }
}
