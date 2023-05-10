using System.Windows.Documents;
using System.Collections.Generic;

namespace GradeBook.Model
{
    public class Student
    {
        public string? ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public int?[] Grades = new int?[5];

        public double Average { get; set; }
        public Student(string id, string firstName, string lastName, string email, string phone, int grade0, int grade1, int grade2, int grade3, int grade4)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Grades[0] = grade0;
            Grades[1] = grade1;
            Grades[2] = grade2;
            Grades[3] = grade3;
            Grades[4] = grade4;
            Average = calcAverage();
        }

        public Student()
        {
        }
        public Student(Student other)
        {
            ID = other.ID;
            FirstName = other.FirstName;
            LastName = other.LastName;
            Email = other.Email;
            Phone = other.Phone;
            Grades[0] = other.Grades[0];
            Grades[1] = other.Grades[1];
            Grades[2] = other.Grades[2];
            Grades[3] = other.Grades[3];
            Grades[4] = other.Grades[4];
            Average = calcAverage();
        }

        public static List<Student> SortStudentsByAverage(List<Student> students)
        {
            /*
             * ******************************
             * BUBBLE SORT
             * ******************************
             */
            //List<Student> arr = new List<Student>(students);
            //Student temp;
            //int n = students.Count;
            //int i, j;
            //bool swapped;
            //for (i = 0; i < n - 1; i++)
            //{
            //    swapped = false;
            //    for (j = 0; j < n - i - 1; j++)
            //    {
            //        if (arr[j].Average > arr[j + 1].Average)
            //        {
            //            temp = arr[j];
            //            arr[j] = arr[j + 1];
            //            arr[j + 1] = temp;
            //            swapped = true;
            //        }
            //    }
            //    if (swapped == false)
            //        break;
            //}

            //return arr;

            //*****************************************************************************

            /*
            * ******************************
            * MERGE SORT
            * ******************************
            */
            List<Student> arr = new List<Student>(students);
            SortByAverage(arr);
            return arr;
            void SortByAverage(List<Student> students)
            {
                if (students.Count <= 1)
                    return;

                List<Student> left = new List<Student>();
                List<Student> right = new List<Student>();

                int mid = students.Count / 2;
                for (int i = 0; i < mid; i++)
                {
                    left.Add(students[i]);
                }
                for (int i = mid; i < students.Count; i++)
                {
                    right.Add(students[i]);
                }

                SortByAverage(left);
                SortByAverage(right);
                Merge(students, left, right);
            }

            void Merge(List<Student> students, List<Student> left, List<Student> right)
            {
                int leftIndex = 0;
                int rightIndex = 0;
                int studentsIndex = 0;

                while (leftIndex < left.Count && rightIndex < right.Count)
                {
                    if (left[leftIndex].Average <= right[rightIndex].Average)
                    {
                        students[studentsIndex] = left[leftIndex];
                        leftIndex++;
                    }
                    else
                    {
                        students[studentsIndex] = right[rightIndex];
                        rightIndex++;
                    }
                    studentsIndex++;
                }

                while (leftIndex < left.Count)
                {
                    students[studentsIndex] = left[leftIndex];
                    leftIndex++;
                    studentsIndex++;
                }

                while (rightIndex < right.Count)
                {
                    students[studentsIndex] = right[rightIndex];
                    rightIndex++;
                    studentsIndex++;
                }
            }

        }
        private double calcAverage()
        {
            double sum = 0;
            int items = 0;
            foreach (var grade in Grades)
            {
                if (grade != 777)
                {
                    sum += (double)grade;
                    items++;
                }
            }
            if (items == 0)
                return 0;
            else return sum / items;
        }
    }
}
