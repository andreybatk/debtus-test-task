using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DebtusTestTask.DB.Entities
{
    public class WorkShift
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? StartShift { get; set; }
        public DateTime? EndShift { get; set; }
        public int HoursWorked { get; set; }
        public int EmployeeId { get; set; }

        [JsonIgnore]
        public virtual Employee? Employee { get; set; }
    }
}