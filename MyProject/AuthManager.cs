using System;
using System.IO;

namespace MyProject
{
    public static class AuthManager
    {
        public static string Token { get; set; }
        public static string UserId { get; set; }
        public static string UserName { get; set; }
        public static string Email { get; set; }

        private static readonly string TokenFilePath = "token.dat";

        public static bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(Token);
        }

        public static void Logout()
        {
            Token = null;
            UserId = null;
            UserName = null;
            Email = null;
            
            if (File.Exists(TokenFilePath))
            {
                File.Delete(TokenFilePath);
            }
        }

        public static void SaveToken()
        {
            try
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    var data = $"{Token}|{UserId}|{UserName}|{Email}";
                    File.WriteAllText(TokenFilePath, data);
                }
            }
            catch (Exception)
            {
            }
        }

        public static bool LoadToken()
        {
            try
            {
                if (File.Exists(TokenFilePath))
                {
                    var data = File.ReadAllText(TokenFilePath);
                    var parts = data.Split('|');
                    
                    if (parts.Length >= 4)
                    {
                        Token = parts[0];
                        UserId = parts[1];
                        UserName = parts[2];
                        Email = parts[3];
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            
            return false;
        }
    }
}
