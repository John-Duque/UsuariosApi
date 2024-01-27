using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Model;

namespace UsuariosApi.Service
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username",usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()), // ClaimTypes contem alguns atributos pre prontos para serem utilizados
                new Claim("LoginTimestamp", DateTime.UtcNow.ToString())
            };

            SymmetricSecurityKey chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("*9ASHDA98H9ah9ha9H9A89n0f*9ASHDA98H9*"));

            SigningCredentials signingCredentials = new SigningCredentials(chave,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}