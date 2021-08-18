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

        public List<Person> haetuutorit() //hakee henkilöistä
        {
            var lista = db.People.Where(p => p.Tutor == true).ToList();

            return (List<Person>)lista;
        }

        public List<Palvelu> haetuutorinpalvelut(int tunniste) //valitse yhden tuutorin monesta samannimisestä tuutorista
        {
            var lista = db.Palvelus.Where(p => p.TutorId == tunniste).ToList();

            return (List<Palvelu>)lista;
        }

        public List<Person> haepalvelutuutorit(int? palvelutunniste) //palveluita palvelutunnisteen perusteella
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

        public void lisääpalvelu(Palvelu palvelu)
        {
            var tuutori = db.People.Find(palvelu.TutorId);

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

        public Person HaeKirjautumistietojaVastaavaHenkilö(string username, string password)
        {
            var person = db.People.Where(k => k.Username == username && k.Password ==password).FirstOrDefault();
            return person;
        }

        public void EditoiPalvelua(Palvelu palvelu)
        {
            var muokattava = db.Palvelus.Find(palvelu.PalveluId);
            muokattava.Hinta = palvelu.Hinta;
            muokattava.Nimi = palvelu.Nimi;
            muokattava.Kesto = palvelu.Kesto;
            muokattava.Pvm = palvelu.Pvm;
            muokattava.Ryhmä = palvelu.Ryhmä;
            muokattava.Sijainti = palvelu.Sijainti;
            muokattava.TutorId = palvelu.TutorId;
            muokattava.Tyyppi = palvelu.Tyyppi;
            muokattava.Varattu = palvelu.Varattu;
        }

        public void EditoiHenkilöä(Person henkilö)
        {
            var muokattava = db.People.Find(henkilö.PersonId);
            muokattava.Osoite = henkilö.Osoite;
            muokattava.Etunimi = henkilö.Etunimi;
            muokattava.Email = henkilö.Email;
            muokattava.Sukunimi = henkilö.Sukunimi;
            muokattava.Postinumero = henkilö.Postinumero;
            muokattava.Postitoimipaikka = henkilö.Postitoimipaikka;
            muokattava.PuhNro = henkilö.PuhNro;
            muokattava.Tutor = henkilö.Tutor;
             
        }
        public void PoistaPalvelu(Palvelu palvelu)
        {
            var poistettava = db.Palvelus.Find(palvelu.PalveluId);
            db.Remove(poistettava);
            db.SaveChanges();
        }
        public void PoistaHenkilö(Person henkilö)
        {
            var poistettava = db.People.Find(henkilö.PersonId);
            db.Remove(poistettava);
            db.SaveChanges();
        }

    }
}
