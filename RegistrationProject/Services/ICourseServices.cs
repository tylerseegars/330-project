using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;

namespace CourseRegistration.Services
{
    public interface ICourseServices
    {
        public List<CourseOffering> getOfferingsByGoalIdAndSemester(String theGoalId, String semester);

        public List<Course> getCourseByName(string name);
        
        public List<Course> getCourses();
        void addCourse(Course course);
        public bool updateCourse(string name, Course updatedCourse);

        public bool deleteCourse(string name);

        public List<CoreGoal> getGoalsByCourse(string name);
        
        public List<CourseOffering> getCourseOfferingsByDept(String department);
        public List<CourseOffering> getCourseOfferingsBySemester(String semester);
        
        public List<CourseOffering> getCourseOfferingsBySemesterAndDept(String semester, String department);     
    }

}
