using GradeBook.Command;
using GradeBook.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace GradeBook.ViewModel
{
    public class StudentViewModel:ValidationViewModelBase
    {
        List<Student> students;
        HashSet<int> existingIds = new HashSet<int>();
        Student temp;
        Random random;

        public StudentViewModel()
        {
            students = new List<Student>();
            temp = new Student();
            SaveOneStudent = new DelegateCommand(SaveStudent);
            AddRandomCommand = new DelegateCommand(AddRandomStudent);
            random = new Random();
            Students = new ObservableCollection<Student>();
        }
        public bool Loading { get; set; }
        public DelegateCommand SaveOneStudent { get; set; }
        public DelegateCommand AddRandomCommand { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public string? ID { 
            get => temp.ID; 
            set {
                temp.ID = value;
                ClearErrors();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
                if (value != null && value.Length != 9)
                {
                    AddError("ID must be 9 numbers long");
                }
                if (!int.TryParse(value, out _))
                {
                    AddError("ID must contain numbers only");
                }
                if(students.Where(s=>s.ID==value).Any())
                {
                    AddError("ID already exists");
                }
            } 
        }

        public string? FirstName
        {
            get => temp.FirstName;
            set
            {
                temp.FirstName = value;
                ClearErrors();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
                if (value != null)
                {
                    //if (value.Length > 20 || value.Length < 1)
                    //{
                    //    AddError("Name need to be 1 to 20 letters long");
                    //}

                    bool isAlpha = Regex.IsMatch(value, @"^[a-zA-Z]+$");

                    if (!isAlpha)
                    {
                        AddError("Name cannot contain numbers or special chars");
                    }
                }
            }
        }

        public string? LastName
        {
            get => temp.LastName;
            set
            {
                temp.LastName = value;
                ClearErrors();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
                if (value != null)
                {
                    //if (value.Length > 20 && value.Length < 1)
                    //{
                    //    AddError("Last name need to be 1 to 20 letters long");
                    //}

                    bool isAlpha = Regex.IsMatch(value, @"^[a-zA-Z]+$");

                    if (!isAlpha)
                    {
                        AddError("Last name cannot contain numbers or special chars");
                    }
                }             
            }
        }

        public string? Email
        {
            get => temp.Email;
            set
            {
                temp.Email = value;
                ClearErrors();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
                if (value != null)
                {
                    bool isValidEmail = Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                    if (!isValidEmail)
                    {
                       AddError("Enter valid email");
                    }
                }
            }
        }

        public string? Phone
        {
            get => temp.Phone;
            set
            {
                temp.Phone = value;
                ClearErrors();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
                if (value != null)
                {
                    if (value.Length != 10)
                    {
                        AddError("Phone number must be 10 numbers long");
                    }
                    if (!int.TryParse(value, out _))
                    {
                        AddError("Phone number must contain numbers only");
                    }
                }
            }
        }

        public string? StrGrades
        {
            get => temp.strGrades;
            set
            {
                temp.strGrades = value;
                ClearErrors();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
                if (value != null)
                {
                    bool isValid = Regex.IsMatch(value, @"^(?:100|[1-9]\d|\d)(?:,(?:100|[1-9]\d|\d)){0,4}$");
                    if (!isValid)
                    {
                        AddError("Enter valid grades. ex. \"86,42,16,100,25\"");
                    }
                    if (isValid)
                    {
                        string[] gradesArray = value.Split(',');
                        int[] grades = Array.ConvertAll(gradesArray, int.Parse);

                        for (int i = 0; i < grades.Length; i++)
                        {
                            temp.Grades[i] = grades[i];                           
                        }

                        for (int i = grades.Length; i < 5; i++)
                        {
                            temp.Grades[i] = 777;
                        }

                    }
                }
            }
        }

        public bool IsSaveAble { get => !HasErrors && ID != null && FirstName != null && LastName != null && Email != null && Phone != null && StrGrades != null; }

        public void AddRandomStudent(object? obj)
        {
            int id,g0,g1,g2,g3,g4;
            string firstname, lastname, email, phone;

            string[] firstnames={
                            "Emma", "Liam", "Olivia", "Noah", "Ava", "Oliver", "Isabella", "Sophia", "Mia", "Charlotte",
                            "Ethan", "Amelia", "Harper", "James", "William", "Abigail", "Evelyn", "Elizabeth", "Michael", "Benjamin",
                            "Ella", "Alexander", "Matthew", "Scarlett", "Emily", "Victoria", "Daniel", "Avery", "Sofia", "Lucas",
                            "Madison", "Aiden", "Chloe", "Mila", "Aria", "Grace", "Harmony", "Isabelle", "Elena", "Audrey",
                            "Hazel", "Eleanor", "Luna", "Lila", "Nora", "Ruby", "Maya", "Adeline", "Leah", "Eliana"
                        };
            string[] lastnames={
                            "Smith", "Johnson", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Martinez", "Hernandez",
                            "Lopez", "Gonzalez", "Perez", "Taylor", "Anderson", "Wilson", "Moore", "Jackson", "Martin", "Lee",
                            "Lewis", "Walker", "Hall", "Young", "Allen", "King", "Wright", "Scott", "Green", "Baker",
                            "Adams", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker",
                            "Evans", "Edwards", "Collins", "Stewart", "Sanchez", "Morris", "Rogers", "Reed", "Cook", "Morgan"
                        };
            
            for (int i=0;i<10000;i++)
            {
                firstname = firstnames[random.Next(0, 50)];
                lastname = lastnames[random.Next(0, 50)];
                email = firstname + "@" + lastname;
                phone = "050" + random.Next(1000000, 10000000).ToString();
                g0 = random.Next(0, 102);
                if (g0 == 101)
                    g0 = 777;
                g1 = random.Next(0, 102);
                if (g1 == 101)
                    g1 = 777;
                g2 = random.Next(0, 102);
                if (g2 == 101)
                    g2 = 777;
                g3 = random.Next(0, 102);
                if (g3 == 101)
                    g3 = 777;
                g4 = random.Next(0, 102);
                if (g4 == 101)
                    g4 = 777;
                do
                {
                    id = random.Next(100000000, 1000000000);
                } while (existingIds.Contains(id));
                existingIds.Add(id);
                students.Add(new Student(id.ToString(), firstname, lastname, email, phone, g0, g1, g2, g3, g4));    
            }
        }
        private void SaveStudent(object? obj)
        {
            students.Add(new Student(temp));
            if (temp.ID != null)
            {
                existingIds.Add(int.Parse(temp.ID));
            }
            ClearStudentForm();

        }
        public void ClearStudentForm()
        {
            temp = new Student();
            RaisePropertyChanged(nameof(ID));
            RaisePropertyChanged(nameof(FirstName));
            RaisePropertyChanged(nameof(LastName));
            RaisePropertyChanged(nameof(Phone));
            RaisePropertyChanged(nameof(Email));
            RaisePropertyChanged(nameof(StrGrades));
            RaisePropertyChanged(nameof(IsSaveAble));
        }

        public void SortStudents()
        {
            Loading= true;
            RaisePropertyChanged(nameof(Loading));
            if (students.Any())
            {
                Students = new ObservableCollection<Student>(Student.SortStudentsByAverage(students));           
            }
            RaisePropertyChanged(nameof(Students));
            Loading = false;
            RaisePropertyChanged(nameof(Loading));
        }
    }
}
