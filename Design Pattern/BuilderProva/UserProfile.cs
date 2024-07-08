namespace WebApi.Design_Pattern.BuilderProva
{
    public class UserProfile
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public bool IsTwoFactorEnabled { get; private set; }
        public string PreferredLanguage { get; private set; }

        private UserProfile() { }

        public static Builder CreateBuilder() => new Builder();

        public class Builder
        {
            private UserProfile _profile = new UserProfile();

            public Builder SetUsername(string username) { _profile.Username = username; return this; }
            public Builder SetEmail(string email) { _profile.Email = email; return this; }
            public Builder SetPhone(string phone) { _profile.Phone = phone; return this; }
            public Builder EnableTwoFactor(bool isEnabled) { _profile.IsTwoFactorEnabled = isEnabled; return this; }
            public Builder SetPreferredLanguage(string language) { _profile.PreferredLanguage = language; return this; }

            public UserProfile Build() => _profile;
        }
    }


    // esempio di utilizzo 

    public class UserProfileTest
    {
        public void EsempioDiUtilizzo()
        {
            var userBuilder = UserProfile.CreateBuilder();
            var userProfile = userBuilder.SetUsername("john_doe")
                                         .SetEmail("john@example.com")
                                         .EnableTwoFactor(true)
                                         .Build();

            var userProfile2 = UserProfile.CreateBuilder()
                                          .SetUsername("john_doe")
                                          .SetEmail("john.doe@example.com")
                                          .EnableTwoFactor(true)
                                          .SetPreferredLanguage("English")
                                          .Build();
        }
    }



}
