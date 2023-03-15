using System;

namespace SkillQuizWebApi.Models
{
    public class TokenResponse
    { 
        public TokenResponse(String accessToken, String refreshToken)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; } = String.Empty;
        public string RefreshToken { get; set; } = String.Empty;
    }
}
