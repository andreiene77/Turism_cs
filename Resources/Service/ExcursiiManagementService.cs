using System;
using System.Collections.ObjectModel;
using System.Linq;
using ServerTurism.Model;
using ServerTurism.Repository;

namespace ServerTurism.Service
{
    public class ExcursiiManagementService
    {
        private readonly IRepository<Excursie, string> repo;

        public ExcursiiManagementService(IRepository<Excursie, string> repo)
        {
            this.repo = repo;
            Excursii = new Collection<Excursie>(repo.FindAll());
            ExcursiiFiltered = new Collection<Excursie>(repo.FindAll());
        }

        public Collection<Excursie> Excursii { get; set; }
        public Collection<Excursie> ExcursiiFiltered { get; set; }

        public void UpdateExcursii() => Excursii = new Collection<Excursie>(repo.FindAll());

        public void UpdateExcursiiFiltered(string obiectiv, TimeSpan oraStart, TimeSpan oraFinish)
        {
            ExcursiiFiltered = new Collection<Excursie>(repo.FindAll().Where(e =>
                e.Obiectiv.Contains(obiectiv)
                && TimeSpan.Compare(e.OraPlecarii, oraStart).Equals(1)
                && TimeSpan.Compare(e.OraPlecarii, oraFinish).Equals(-1)).ToList());
        }

        public void UpdateExcursie(Excursie excursie)
        {
            repo.Update(excursie);
            UpdateExcursii();
        }
    }
}