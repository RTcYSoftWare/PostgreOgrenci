using PostgreOgrenci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreOgrenci.Services.Abstract
{
    public interface IAdministrator
    {
        OgrenciToken Authenticate(string kullaniciAdi, string sifre);
    }
}
