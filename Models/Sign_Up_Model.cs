namespace Personal_Income.Models
{
    public class Sign_Up_Model
    {
        public string? UserName { get; set; }

        public string? UserLastName { get; set; }

        public string? UserPhoneNumber { get; set; }

        public string? UserEmailAddress { get; set; }

        public string? Password { get; set; }

        public string? PasswordConfirm { get; set; }

        public bool IsPasswordConfirmed { get; set; }

    }
}
