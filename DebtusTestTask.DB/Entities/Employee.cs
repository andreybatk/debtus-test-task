using System.ComponentModel.DataAnnotations.Schema;

namespace DebtusTestTask.DB.Entities
{
    public class Employee
    {
        public Employee()
        {
            WorkShifts = new List<WorkShift>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public virtual List<WorkShift> WorkShifts { get; set; }
    }
}