using System;
using ServerTurism.Model;
using ServerTurism.Repository;

namespace ServerTurism.Service
{
    public class RezervareService
    {
        private readonly ExcursiiManagementService excursiiManagementService;
        private readonly IRepository<Rezervare, string> repoRez;

        public RezervareService(ExcursiiManagementService excursiiManagementService,
            IRepository<Rezervare, string> repoRez)
        {
            this.excursiiManagementService = excursiiManagementService;
            this.repoRez = repoRez;
        }

        public Agentie User { get; set; }

        public void Rezerva(Excursie excursie, string nume, string telefon, int nr_bilete)
        {
            if (excursie.LocuriDisponibile >= nr_bilete)
            {
                excursie.LocuriDisponibile -= nr_bilete;
                var id = User.GetHashCode() + excursie.GetHashCode() + DateTime.Now.GetHashCode();
                var rezervare = new Rezervare(id.ToString(), User, excursie, nume, telefon, nr_bilete);
                repoRez.Add(rezervare);
                excursiiManagementService.UpdateExcursie(excursie);
            }
            else
            {
                throw new Exception("date invalide");
            }
        }
    }
}