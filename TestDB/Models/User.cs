namespace TestDB.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.Now;

    }
}
