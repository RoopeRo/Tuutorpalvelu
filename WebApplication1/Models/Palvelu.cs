using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Palvelu
    {
        public int PalveluId { get; set; }
        public string Nimi { get; set; }
        public string Tyyppi { get; set; }
        public string Ryhmä { get; set; }
        public decimal Hinta { get; set; }
        public DateTime Pvm { get; set; }
        public int Kesto { get; set; }
        public string Sijainti { get; set; }
        public bool Varattu { get; set; }
        public int? TutorId { get; set; }

        public virtual Person Tutor { get; set; }
    }
}
