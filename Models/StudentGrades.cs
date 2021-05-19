using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentGrades.Models
{
    public class StudentGrades
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("Student")]
        public string Name { get; set; }

        public double Grade { get; set; }

        [DisplayName("Saved on")]
        public DateTime? ReportingDate
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }

        private DateTime? dateCreated = null;
    }
}