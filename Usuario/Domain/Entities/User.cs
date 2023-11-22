namespace Usuario.Domain.Entities
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
