using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using API.Interfaces;

namespace API.Services;

public class TokenService(IConfiguration config) : Interfaces.ITokenService
{
    public string CreateToken(Entities.AppUser user)
    {
        var tokenKey= config["TokenKey"] ?? throw new Exception("Cannot access token key from appsettings");
        if(tokenKey.Length<24)throw new Exception("Your token key needs to be longer");
        var key= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));
        var claims= new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName)
        };  
        
        var creds=new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor= new SecurityTokenDescriptor
        {
            Subject=new ClaimsIdentity(claims),
            Expires=DateTime .UtcNow.AddDays(7),
            SigningCredentials=creds
        };

        var tokenHandler= new JwtSecurityTokenHandler();
        var token= tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
