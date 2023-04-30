using GradeBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (value!=null && value.Length > 20 && value.Length < 1)
                {
                    AddError("test");
                }
                foreach (var item in value.ToCharArray())
                {
                    if (Char.IsDigit(item))
                        AddError("test");
                }
            }
        }
    }
}
