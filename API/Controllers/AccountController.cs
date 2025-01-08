using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDto)
    {
        if (await UserExists(registerDto.username)) return BadRequest("Username exists");
        using var hmac=new HMACSHA512();
        var user= new AppUser{
            UserName=registerDto.username,
            PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
            PasswordSalt=hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }
    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x=>x.UserName.ToLower()==username.ToLower());
    }
    [HttpPost("login")]
    public async Task<ActionResult<AppUser>> Login(LoginDTO loginDto)
    {
        var user=await context.Users.FirstOrDefaultAsync(x=>x.UserName==loginDto.username.ToLower());
        if(user==null) return Unauthorized("Invalid username");
        using var hmac=new HMACSHA512(user.PasswordSalt);
        var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
        for(int i=0;i<computedHash.Length;i++)
        {
            if(computedHash[i]!=user.PasswordHash[i])return Unauthorized("Invalid password");
        }
        return user;
    }
}
