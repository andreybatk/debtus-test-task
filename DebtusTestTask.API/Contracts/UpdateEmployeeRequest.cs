namespace DebtusTestTask.API.Contracts
{
    public class UpdateEmployeeRequest
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
    }
}