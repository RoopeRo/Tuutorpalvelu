using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1
{
    public class DataAccess
    {
        public TutorpalveluDBContext db { get; set; }
        public DataAccess(TutorpalveluDBContext data)
        {
            db = data;
        }

        public List<Palvelu> haepalvelut()
        {

            var Lista = db.Palvelus.Where(p => p.PalveluId != -1).ToList();

            return (List<Palvelu>)Lista;
        }

        public List<Person> haetuutorit()
        {
            var lista = db.People.Where(p => p.Tutor == true).ToList();

            return (List<Person>)lista;
        }

        public List<Palvelu> haetuutorinpalvelut(int tunniste)
        {
            var lista = db.Palvelus.Where(p => p.TutorId == tunniste).ToList();

            return (List<Palvelu>)lista;
        }

        public List<Person> haepalvelutuutorit(int palvelutunniste)
        {
            var peeple = db.People.Include(p => p.Palvelus).ToList();

            var peple = (from p in peeple
                        from k in p.Palvelus
                        where k.PalveluId == palvelutunniste
                        select p).ToList();
           

            return (List<Person>)peple;
        }

        public void Lisääkäyttäjä(Person p)
        {
            db.People.Add(p);
            db.SaveChanges();
        }

        public void lisääpalvelu(Palvelu palvelu, int henkilöid)
        {
            Person tuutori = (Person)db.People.Where(p => p.PersonId == henkilöid);

            tuutori.Palvelus.Add(palvelu);

            db.People.Update(tuutori);
            db.SaveChanges();

        }

        public bool TarkistaKäyttäjänAuth(string username, string password)
        {
            if (db.People.Where(k => k.Username == username).FirstOrDefault() != null && db.People.Where(k => k.Username == username).FirstOrDefault().Password == password)
            {
                return true;
            }
            return false;
        }


    }
}
