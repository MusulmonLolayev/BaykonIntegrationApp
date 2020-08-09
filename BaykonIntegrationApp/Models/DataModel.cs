using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaykonIntegrationApp.Models
{
    public class DataModel : AbstractModel
    {
        public Nomenclature Nomenclature { get; set; }
        public Shift Shift { get; set; }
        public DateTime Date { get; set; }
        public PackageView PackageView { get; set; }
        public ConeColour ConeColour { get; set; }
        public Lot Lot { get; set; }
        public double WeightBox { get; set; }

        public double WeightNetto { get { return WeightBrutto - WeightBox; } }

        private double weightBrutto;
        public double WeightBrutto
        {
            get { return weightBrutto; }
            set { weightBrutto = value;
                OnPropertyChanged("WeightBrutto");
                OnPropertyChanged("WeightBox");
                OnPropertyChanged("WeightNetto");
            }
        }

        public double StandartWeight { get; set; }

        public DataModel(Nomenclature Nomenclature, Shift Shift, DateTime Date, PackageView PackageView, ConeColour ConeColour,
            Lot Lot, double WeightBox, double WeightBrutto)
        {
            this.Nomenclature = Nomenclature;
            this.Shift = Shift;
            this.Date = Date;
            this.PackageView = PackageView;
            this.ConeColour = ConeColour;
            this.Lot = Lot;
            this.WeightBox = WeightBox;
            this.WeightBrutto = WeightBrutto;
        }
        public DataModel()
        {
            Nomenclature = new Nomenclature();
            Shift = new Shift();
            PackageView = new PackageView();
            ConeColour = new ConeColour();
            Lot = new Lot();
        }

        public override string ToString()
        {
            return 
                $"{Nomenclature.Name}-" +
                $"{Lot.Name}-" +
                $"{Lot.LastNumber}-" +
                $"{Date.Day + "." + Date.Month + "." + Date.Year}";
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
