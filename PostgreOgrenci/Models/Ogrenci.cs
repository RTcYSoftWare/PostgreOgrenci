using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreOgrenci.Models
{
    public class Ogrenci
    {
        [Key]
        public int Numara { get; set; }
        public int BolumId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string DanismanAdi { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string Token { get; set; }

    }
}
