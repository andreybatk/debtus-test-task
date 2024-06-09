namespace DebtusTestTask.API.Contracts
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public List<WorkShiftResponse>? WorkShifts { get; set; }
    }
}