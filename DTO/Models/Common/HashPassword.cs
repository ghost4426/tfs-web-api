namespace DTO.Models
{
    public class HashPassword
    {
        public string Password { get; set; }

        public string Salt { get; set; }

        public string HashedPassword { get; set; }
    }
}
