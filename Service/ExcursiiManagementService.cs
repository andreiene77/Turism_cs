using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Turism_cs.Repository;

namespace Turism_cs.Service
{
    public class ExcursiiManagementService : INotifyPropertyChanged
    {
        private IRepository<Excursie, string> repo;
        private readonly ObservableCollection<Excursie> excursii;
        private readonly ObservableCollection<Excursie> excursiiFiltered;

        public Agentie User { get; set; }
        public ObservableCollection<Excursie> Excursii
        {
            get
            {
                OnPropertyChanged("Excursii");
                return excursii;
            }
        }

        public ObservableCollection<Excursie> ExcursiiFiltered
        {
            get
            {
                OnPropertyChanged("ExcursiiFiltered");
                return excursiiFiltered;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ExcursiiManagementService(IRepository<Excursie, string> repo)
        {
            this.repo = repo;
            excursii = new ObservableCollection<Excursie>(repo.FindAll());
            excursiiFiltered = new ObservableCollection<Excursie>(repo.FindAll());
        }

        public ObservableCollection<Excursie> GetExcursieList()
        {
            var list = new ObservableCollection<Excursie>(repo.FindAll());
            return list;
        }

        public void UpdateExcursii()
        {
            Excursii.Clear();
            foreach (Excursie e in repo.FindAll()) Excursii.Add(e);
            OnPropertyChanged("Excursii");
        }

        public ObservableCollection<Excursie> GetSearchList(string obiectiv, TimeSpan oraStart, TimeSpan oraFinish)
        {
            var list = new ObservableCollection<Excursie>(
            repo.FindAll().Where(e =>
            e.Obiectiv.Contains(obiectiv)
            && TimeSpan.Compare(e.OraPlecarii, oraStart).Equals(1)
            && TimeSpan.Compare(e.OraPlecarii, oraFinish).Equals(-1))
            );
            return list;
        }

        public void UpdateExcursiiFiltered(string obiectiv, TimeSpan oraStart, TimeSpan oraFinish)
        {
            ExcursiiFiltered.Clear();
            var filteredList = repo.FindAll().Where(e =>
            e.Obiectiv.Contains(obiectiv)
            && TimeSpan.Compare(e.OraPlecarii, oraStart).Equals(1)
            && TimeSpan.Compare(e.OraPlecarii, oraFinish).Equals(-1));
            foreach (Excursie e in filteredList) ExcursiiFiltered.Add(e);
            OnPropertyChanged("ExcursiiFiltered");
        }

        public void UpdateExcursie(Excursie excursie)
        {
            repo.Update(excursie);
            UpdateExcursii();
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
