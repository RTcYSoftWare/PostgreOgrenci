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

        public OgrenciToken Authenticate(string OgrenciNo, string sifre)
        {
            //var user = _ctxpost.ogrenciToken.Where(s => s.ogrenciNo == OgrenciNo);
            //var user = _ctxpost.ogrenciToken.Find(41);

            var user = _ctxpost.ogrenciToken.Where(s => s.ogrenciNo == OgrenciNo && s.sifre == sifre).FirstOrDefault();
                       

            if (user == null)
                return null;

            // Authentication(Yetkilendirme) başarılı ise JWT token üretilir.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            user.token = tokenHandler.WriteToken(token);
            _ctxpost.SaveChanges();

            // Sifre null olarak gonderilir.
            user.sifre = null;

            return user;
        }
    }
}
