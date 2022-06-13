using DogsCompanion.App.Models.Personal;
using DogsCompanion.App.Models.Update;

namespace DogsCompanion.App.Models.Account
{
    public class RegisterResponse
    {
        public bool IsEmailInUse { get; set; }
        public bool IsPhoneInUse { get; set; }

        public UserInfo? UserInfo { get; set; }
        public ReadDog? DogInfo { get; set; }

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
