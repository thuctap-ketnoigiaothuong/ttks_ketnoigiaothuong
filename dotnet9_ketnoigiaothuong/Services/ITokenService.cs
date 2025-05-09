﻿using dotnet9_ketnoigiaothuong.Domain.Entities;

namespace dotnet9_ketnoigiaothuong.Services
{
    public interface ITokenService
    {
        public string GenerateJwtToken(UserAccount user);
        public bool IsTokenValid(string token);
    }
}
