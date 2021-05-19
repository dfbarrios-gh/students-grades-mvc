using System.ComponentModel;

namespace StudentGrades.ViewModels
{
    public class StudentGradeViewModel
    {
        [DisplayName("Full Name")]
        public string Name { get; set; }

        [DisplayName("Final Grade")]
        public double FinalGrade { get; set; }
    }
}