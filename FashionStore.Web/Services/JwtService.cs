using FashionStore.Models.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FashionStore.Web.Services;

public class JwtService
{
    private readonly string _secret;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryInMinutes;

    public JwtService(IConfiguration configuration)
    {
        _secret = configuration["Jwt:Key"];
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
        _expiryInMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]);
    }

    public string GenerateJwtToken(ApplicationUser user, IList<string> roles)
    {
        try
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_expiryInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            // Log the error
            // You can use a logging framework like Serilog, NLog, etc.
            Console.WriteLine($"JWT Generation Error: {ex.Message}");
            throw new Exception("An error occurred while generating the token.");
        }
    }















    //public string GenerateJwtToken(ApplicationUser user, IList<string> roles)
    //{
    //    var claims = new List<Claim>
    //    {
    //        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
    //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
    //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //        new Claim(ClaimTypes.Name, user.UserName)
    //    };

    //    // Add user roles as claims
    //    foreach (var role in roles)
    //    {
    //        claims.Add(new Claim(ClaimTypes.Role, role));
    //    }

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: _issuer,
    //        audience: _audience,
    //        claims: claims,
    //        expires: DateTime.Now.AddMinutes(_expiryInMinutes),
    //        signingCredentials: creds);

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}
}
