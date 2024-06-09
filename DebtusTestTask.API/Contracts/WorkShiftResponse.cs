namespace DebtusTestTask.API.Contracts
{
    public class WorkShiftResponse
    {
        public int Id { get; set; }
        public DateTime? StartShift { get; set; }
        public DateTime? EndShift { get; set; }
        public int HoursWorked { get; set; }
    }
}