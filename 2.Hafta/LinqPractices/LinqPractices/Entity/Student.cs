using System.ComponentModel.DataAnnotations.Schema;

namespace LinqPractices.Entity
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ClassID { get; set; }
    }
}
