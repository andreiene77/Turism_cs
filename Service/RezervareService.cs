using System;
using Turism_cs.Repository;

namespace Turism_cs.Service
{
    public class RezervareService
    {
        private ExcursiiManagementService excursiiManagementService;
        private IRepository<Rezervare, string> repoRez;
        public Agentie User { get; set; }

        public RezervareService(ExcursiiManagementService excursiiManagementService, IRepository<Rezervare, string> repoRez)
        {
            this.excursiiManagementService = excursiiManagementService;
            this.repoRez = repoRez;
        }

        public void Rezerva(Excursie excursie, string nume, string telefon, int nr_bilete)
        {
            if (excursie.LocuriDisponibile >= nr_bilete)
            {
                excursie.LocuriDisponibile -= nr_bilete;
                int id = User.GetHashCode() + excursie.GetHashCode() + DateTime.Now.GetHashCode();
                Rezervare rezervare = new Rezervare(id.ToString(), User, excursie, nume, telefon, nr_bilete);
                repoRez.Add(rezervare);
                excursiiManagementService.UpdateExcursie(excursie);
            }
            else throw new Exception("date invalide");
        }
    }
}
