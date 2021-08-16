using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Person
    {
        public Person()
        {
            Palvelus = new HashSet<Palvelu>();
        }

        public int PersonId { get; set; }
        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public string Osoite { get; set; }
        public string Postitoimipaikka { get; set; }
        public string Postinumero { get; set; }
        public string PuhNro { get; set; }
        public string Email { get; set; }
        public bool Tutor { get; set; }

        public virtual ICollection<Palvelu> Palvelus { get; set; }
    }
}
