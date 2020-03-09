using PostgreOgrenci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreOgrenci.Services.Abstract
{
    public interface IAdministrator
    {
        Ogrenci Authenticate(int kullaniciAdi, string sifre);
    }
}
