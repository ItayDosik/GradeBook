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

            //    // IF no two elements were swapped by inner loop, then break
            //    if (swapped == false)
            //        break;
            //}

            //return arr;

            //*****************************************************************************

            /*
            * ******************************
            * RADIX SORT
            * ******************************
            */
            var arr = students.ToArray();
            double maxAverage = arr[0].Average;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i].Average > maxAverage)
                {
                    maxAverage = arr[i].Average;
                }
            }

            // Do counting sort for every digit, starting from least significant to most significant
            for (int exp = 1; maxAverage / exp > 0; exp *= 10)
            {
                CountingSortByAverage(arr, exp);
            }
            return new List<Student>(arr);

            void CountingSortByAverage(Student[] arr, int exp)
            {
                Student[] output = new Student[arr.Length];
                int[] count = new int[10];

                // Count occurrences of digits at the current exponent
                for (int i = 0; i < arr.Length; i++)
                {
                    int digit = (int)((arr[i].Average / exp) % 10);
                    count[digit]++;
                }

                // Calculate the cumulative count of digits
                for (int i = 1; i < count.Length; i++)
                {
                    count[i] += count[i - 1];
                }

                // Build the output array
                for (int i = arr.Length - 1; i >= 0; i--)
                {
                    int digit = (int)((arr[i].Average / exp) % 10);
                    output[count[digit] - 1] = arr[i];
                    count[digit]--;
                }

                // Copy the output array back to the input array
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = output[i];
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
        private static void CountingSortByAverage(Student[] arr, int exp)
        {
            Student[] output = new Student[arr.Length];
            int[] count = new int[10];

            // Count occurrences of digits at the current exponent
            for (int i = 0; i < arr.Length; i++)
            {
                int digit = (int)((arr[i].Average / exp) % 10);
                count[digit]++;
            }

            // Calculate the cumulative count of digits
            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }

            // Build the output array
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                int digit = (int)((arr[i].Average / exp) % 10);
                output[count[digit] - 1] = arr[i];
                count[digit]--;
            }

            // Copy the output array back to the input array
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = output[i];
            }
        }
    }
}
