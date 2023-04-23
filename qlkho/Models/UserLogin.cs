namespace qlkho.Models
{
    public class UserLogin
    {
        public string? Username { get; set; }

        public string Password { get; set; }

        internal static Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
