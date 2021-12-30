namespace fr.Service.Model.Account
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string StudentCode { get; set; }
        public DateTime DateOfBird { get; set; }
        public string Token { get; set; }
        public string Schema { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Claims { get; set; }

        public string FullName { get => $"{FirstName} {LastName}".Trim(); }
    }
}
