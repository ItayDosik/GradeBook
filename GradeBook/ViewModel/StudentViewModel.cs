using GradeBook.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace GradeBook.ViewModel
{
    public class StudentViewModel:ValidationViewModelBase
    {
        List<Student> students = new List<Student>();
        Student temp = new Student();
        public string? ID { 
            get => temp.ID; 
            set {
                temp.ID = value;
                ClearErrors();
                RaisePropertyChanged();
                if (value != null && value.Length != 9)
                {
                    AddError("ID must be 9 numbers long");
                }
                if (!int.TryParse(value, out _))
                {
                    AddError("ID must contain numbers only");
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
                if(value != null)
                {
                    if (value.Length > 20 || value.Length < 1)
                    {
                        AddError("Name need to be 1 to 20 letters long");
                    }

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
                if (value != null)
                {
                    if (value.Length > 20 && value.Length < 1)
                    {
                        AddError("Last name need to be 1 to 20 letters long");
                    }

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
    }
}
