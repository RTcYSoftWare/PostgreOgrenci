using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreOgrenci.Models
{
    public class OgrenciToken
    {
        public int Id { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string ogrenciNo { get; set; }
        public string email { get; set; }
        public string sifre { get; set; }
        public string token { get; set; }

    }
}
