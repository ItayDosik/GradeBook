using GradeBook.Command;
using GradeBook.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GradeBook.ViewModel
{
    public class StudentViewModel:ValidationViewModelBase
    {
        private List<Student> students;
        private Student temp;
        private Random random;
        private HashSet<string> idSet;

        public StudentViewModel()
        {
            students = new List<Student>();
            temp = new Student();
            SaveOneStudent = new DelegateCommand(SaveStudent);
            AddRandomCommand = new DelegateCommand(AddRandomStudentAsync);
            random = new Random();
            Students = new ObservableCollection<Student>();
            idSet = new HashSet<string>();
            Loading = false;
        }
        public DelegateCommand SaveOneStudent { get; set; }
        public DelegateCommand AddRandomCommand { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public string? ID { 
            get => temp.ID; 
            set {
                temp.ID = value;
                ClearErrors();

                if (!long.TryParse(value, out _))
                {
                    AddError("ID must contain numbers only");
                }
                if (value != null && value.Length != 9)
                {
                    AddError("ID must be 9 numbers long");
                }
               
                if(idSet.Contains(value))
                {
                    AddError("ID already exists");
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            } 
        }

        public string? FirstName
        {
            get => temp.FirstName;
            set
            {
                temp.FirstName = value;
                ClearErrors();

                if (value != null)
                {
                    bool isAlpha = Regex.IsMatch(value, @"^[a-zA-Z]+$");
                    if (!isAlpha)
                    {
                        AddError("Name cannot contain numbers or special chars");
                    }
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            }
        }

        public string? LastName
        {
            get => temp.LastName;
            set
            {
                temp.LastName = value;
                ClearErrors();
    
                if (value != null)
                {
                    bool isAlpha = Regex.IsMatch(value, @"^[a-zA-Z]+$");

                    if (!isAlpha)
                    {
                        AddError("Last name cannot contain numbers or special chars");
                    }
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            }
        }

        public string? Email
        {
            get => temp.Email;
            set
            {
                temp.Email = value;
                ClearErrors();
                if (value != null)
                {
                    bool isValidEmail = value.Contains("@");
                    if (!isValidEmail)
                    {
                       AddError("Email must contain @");
                    }
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            }
        }

        public string? Phone
        {
            get => temp.Phone;
            set
            {
                temp.Phone = value;
                ClearErrors();

                if (value != null)
                {
                   
                    if (!long.TryParse(value, out _))
                    {
                        AddError("Phone number must contain numbers only");
                    }
                    if (value.Length != 10)
                    {
                        AddError("Phone number must be 10 numbers long");
                    }
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            }
        }

        public string? Grade1 { 
            get=>temp.Grades[0].ToString(); 
            set {
                int grade;
                if (int.TryParse(value, out grade) && ((grade >= 0 && grade <= 100) || grade == 777))
                {
                    ClearErrors();
                    temp.Grades[0] = grade;
                }
                else
                {
                    AddError("0 - 100 or 777");
                    temp.Grades[0] = null;
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            } 
        }
        public string? Grade2
        {
            get => temp.Grades[1].ToString();
            set
            {
                int grade;
                if (int.TryParse(value, out grade) && ((grade >= 0 && grade <= 100) || grade == 777))
                {
                    ClearErrors();
                    temp.Grades[1] = grade;
                }
                else
                {
                    AddError("0 - 100 or 777");
                    temp.Grades[1] = null;
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            }
        }
        public string? Grade3
        {
            get => temp.Grades[2].ToString();
            set
            {
                int grade;
                if (int.TryParse(value, out grade) && ((grade >= 0 && grade <= 100) || grade == 777))
                {
                    ClearErrors();
                    temp.Grades[2] = grade;
                }
                else
                {
                    AddError("0 - 100 or 777");
                    temp.Grades[2] = null;
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            }
        }
        public string? Grade4
        {
            get => temp.Grades[3].ToString();
            set
            {
                int grade;
                if (int.TryParse(value, out grade) && ((grade >= 0 && grade <= 100) || grade == 777))
                {
                    ClearErrors();
                    temp.Grades[3] = grade;
                }
                else
                {
                    AddError("0 - 100 or 777");
                    temp.Grades[3] = null;
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            }
        }
        public string? Grade5
        {
            get => temp.Grades[4].ToString();
            set
            {
                int grade;
                if (int.TryParse(value, out grade) && ((grade >= 0 && grade <= 100) || grade == 777))
                {
                    ClearErrors();
                    temp.Grades[4] = grade;
                }
                else
                {
                    AddError("0 - 100 or 777");
                    temp.Grades[4] = null;
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSaveAble));
            }
        }
        public bool Loading {get; set;}
        public bool IsSaveAble { get => !HasErrors && ID != null && FirstName != null && LastName != null && Email != null && Phone != null && Grade1 != "" && Grade2 != "" && Grade3 != "" && Grade4 != "" && Grade5 != ""; }
        public string SortTime { get; private set; }

        public async void AddRandomStudentAsync(object? obj)
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
                id = random.Next(100000000, 1000000000);
                while(idSet.Contains(id.ToString()))
                    id = random.Next(100000000, 1000000000);
                students.Add(new Student(id.ToString(), firstname, lastname, email, phone, g0, g1, g2, g3, g4)); 
                idSet.Add(id.ToString());
            }
            DialogHost.GetDialogSession("rootDialog").UpdateContent(obj);
            await Task.Run(() =>
            {
                SortStudents();
                
            });

            
        }
        private void SaveStudent(object? obj)
        {
            students.Add(new Student(temp));
            idSet.Add(temp.ID);
            ClearStudentForm();
            DialogHost.OpenDialogCommand.Execute(obj, null);
        }
        public void ClearStudentForm()
        {
            temp = new Student();
            RaisePropertyChanged(nameof(ID));
            RaisePropertyChanged(nameof(FirstName));
            RaisePropertyChanged(nameof(LastName));
            RaisePropertyChanged(nameof(Phone));
            RaisePropertyChanged(nameof(Email));
            RaisePropertyChanged(nameof(Grade1));
            RaisePropertyChanged(nameof(Grade2));
            RaisePropertyChanged(nameof(Grade3));
            RaisePropertyChanged(nameof(Grade4));
            RaisePropertyChanged(nameof(Grade5));
            RaisePropertyChanged(nameof(IsSaveAble));
        }

        public void SortStudents()
        {
            DateTime time = DateTime.Now;
            TimeSpan timeSpan;
            Loading =true;
            SortTime = "0";
            RaisePropertyChanged(nameof(Loading));
            RaisePropertyChanged(nameof(SortTime));
            if (students.Any())
            {

                time = DateTime.Now;
                Students = new ObservableCollection<Student>(Student.SortStudentsByAverage(students));
                timeSpan = DateTime.Now-time;
                SortTime = timeSpan.TotalSeconds.ToString();
                RaisePropertyChanged(nameof(SortTime));        

            }
            RaisePropertyChanged(nameof(Students));
            Loading = false;
            RaisePropertyChanged(nameof(Loading));           
        }
    }
}
