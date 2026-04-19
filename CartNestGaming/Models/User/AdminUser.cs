namespace CartNestGaming.Models
{

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        // Role: Admin or User
        public string Role { get; set; }
    }
}
