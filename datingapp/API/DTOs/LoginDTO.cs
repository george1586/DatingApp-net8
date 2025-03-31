using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Identity;

namespace API.DTOs;

public class LoginDTO
{
    [Required]
    public required string username{get; set;}
    [Required]
    public required string password{get; set;}
}
