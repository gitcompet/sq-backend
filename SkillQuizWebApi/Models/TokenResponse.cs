using System;

namespace SkillQuizWebApi.Models
{
    public class TokenResponse
    { 
        public TokenResponse(string accessToken, string refreshToken)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
