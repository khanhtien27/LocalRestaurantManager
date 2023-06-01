namespace ManagementRestaurentWeb.Models.ModelDTO
{
    public class LoginResponseDTO
    {
        public LocalUserDTO User { get; set; }
        public string Token { get; set; }
    }
}
