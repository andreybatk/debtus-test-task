using System.ComponentModel.DataAnnotations;

namespace DebtusTestTask.API.Contracts
{
    public class NewEmployeeRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string JobTitle { get; set; }
    }
}