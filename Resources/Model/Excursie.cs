using System;
using System.ComponentModel;

namespace ServerTurism.Model
{
    [Serializable]
    public class Excursie : IHasId<string>, INotifyPropertyChanged
    {
        private int locuriDisponibile;

        public Excursie(string id, string obiectiv, string firmaTransport, TimeSpan oraPlecarii, double pretul,
            int locuriDisponibile)
        {
            Id = id;
            Obiectiv = obiectiv;
            FirmaTransport = firmaTransport;
            OraPlecarii = oraPlecarii;
            Pretul = pretul;
            LocuriDisponibile = locuriDisponibile;
        }

        public string Obiectiv { get; set; }
        public string FirmaTransport { get; set; }
        public TimeSpan OraPlecarii { get; set; }
        public double Pretul { get; set; }

        public int LocuriDisponibile
        {
            get => locuriDisponibile;
            set
            {
                locuriDisponibile = value;
                OnPropertyChanged("LocuriDisponibile");
            }
        }

        public string Id { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}