﻿namespace UalaSelecionado8.Models
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}