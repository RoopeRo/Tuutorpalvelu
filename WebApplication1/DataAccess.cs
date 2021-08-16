﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1
{
    public class DataAccess
    {
        TutorpalveluDBContext db = new TutorpalveluDBContext();

        public List<Palvelu> haepalvelut()
        {

            var Lista = db.Palvelus.Where(p => p.PalveluId != -1);

            return (List<Palvelu>)Lista;
        }

        public List<Person> haetuutorit()
        {
            var lista = db.People.Where(p => p.Tutor == true);

            return (List<Person>)lista;
        }

        public List<Palvelu> haetuutorinpalvelut(int tunniste)
        {
            var lista = db.Palvelus.Where(p => p.TutorId == tunniste);

            return (List<Palvelu>)lista;
        }

        public List<Person> haepalvelutuutorit(int palvelutunniste)
        {
            var peeple = db.People.Include(p => p.Palvelus).ToList();

            var peple = from p in peeple
                        from k in p.Palvelus
                        where k.PalveluId == palvelutunniste
                        select p;
           

            return (List<Person>)peple;
        }

        public void lisäätuutori(Person p)
        {
            db.People.Add(p);
            db.SaveChanges();
        }

        public void lisääpalvelu(Palvelu p, int tuutoriid)
        {
            var tuutori = db.People.Find(tuutoriid);

            tuutori.Palvelus.Add(p);

            db.People.Update(tuutori);
            db.SaveChanges();

        }


    }
}
