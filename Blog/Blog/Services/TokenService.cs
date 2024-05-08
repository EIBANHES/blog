using Blog.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Text;

namespace Blog.Services;

public class TokenService
{
    // Finalidade de gerar token

    public string GenerateToken(User user0)
    {
        // Gera o token
        var tokenHandler = new JwtSecurityTokenHandler();
        // Pega a chave
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey); // array de bytes
        //contem todas as informações do token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        // Cria token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        // gera uma string baseada no token
        return tokenHandler.WriteToken(token);

    }
}
