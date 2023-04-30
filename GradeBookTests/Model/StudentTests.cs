using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GradeBook.Model.Tests
{
    [TestClass()]
    public class StudentTests
    {
        [TestMethod()]
        public void StudentAverageTest()
        {
            Student st1 = new Student("100000000", "st1", "student", "st1@gmail.com", "0500000000", 20, 30, 100, 50, 70);
            Student st2 = new Student("200000000", "st2", "student", "st2@gmail.com", "0500000000", 20, 30, 777, 777, 70);

            Assert.AreEqual(54, st1.Average, "Wrong average calculation");
            Assert.AreEqual(40, st2.Average, "Wrong average calculation");

        }

        [TestMethod()]
        public void SortStudentsByAverageTest()
        {
            Random random= new Random();
            List<Student> students = new List<Student>();
            students.Add(new Student("100000000", "st1", "student", "st1@gmail.com", "0500000000", 20, 30, 100, 50, 70));
            students.Add(new Student("200000000", "st2", "student", "st2@gmail.com", "0500000000", 15,20,30,48,14));
            students.Add(new Student("300000000", "st3", "student", "st3@gmail.com", "0500000000", 90,87,65,87,777));
            students.Add(new Student("400000000", "st4", "student", "st4@gmail.com", "0500000000",90,75,30,24,87));
            students.Add(new Student("500000000", "st5", "student", "st5@gmail.com", "0500000000", 777,55,87,65,89));
            students.Add(new Student("600000000", "st6", "student", "st6@gmail.com", "0500000000",56,777,58,777,89));
            List<Student> result = Student.SortStudentsByAverage(students);
            Assert.AreNotEqual(null,result,"Sort returned null");
            CollectionAssert.AreEquivalent(students,result,"the two list are not equivalent");
            for(int i = 0; i < 5; i++)
            {
                Assert.IsTrue(result[i].Average <= result[i + 1].Average, "The result is not sorted well");
            }
        }
    }
}