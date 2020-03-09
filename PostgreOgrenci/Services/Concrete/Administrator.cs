using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PostgreOgrenci.Models;
using PostgreOgrenci.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PostgreOgrenci.Services
{
    public class Administrator : IAdministrator
    {
        private readonly PostgresContext _ctxpost;
        private readonly AppSettings _appSettings;

        public Administrator(PostgresContext ctxpost, IOptions<AppSettings> appSettings)
        {
            _ctxpost = ctxpost;
            _appSettings = appSettings.Value;
        }

        public Administrator(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Ogrenci Authenticate(int Numara, string Sifre)
        {
            var user = _ctxpost.Ogrenci.Where(s => s.Numara == Numara && s.Sifre == Sifre).FirstOrDefault();
                       
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Numara.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            user.Token = tokenHandler.WriteToken(token);
            _ctxpost.SaveChanges();

            return user;
        }

    }
}
