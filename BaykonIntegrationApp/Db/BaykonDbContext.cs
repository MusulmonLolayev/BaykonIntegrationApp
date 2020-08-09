using BaykonIntegrationApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaykonIntegrationApp.Db
{
    class BaykonDbContext : IDisposable
    {
        public List<Nomenclature> Nomenclatures
        {
            get
            {
                List<Nomenclature> list = new List<Nomenclature>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Nomenclature").Rows)
                {
                    list.Add(new Nomenclature(data));
                }
                return list;
            }
            private set {; }
        }

        public List<Shift> Shifts
        {
            get
            {
                List<Shift> list = new List<Shift>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Shift").Rows)
                {
                    list.Add(new Shift(data));
                }
                return list;
            }
            private set {; }
        }

        public List<PackageView> PackageViews
        {
            get
            {
                List<PackageView> list = new List<PackageView>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From PackageView").Rows)
                {
                    list.Add(new PackageView(data));
                }
                return list;
            }
            private set {; }
        }

        public List<ConeColour> ConeColours
        {
            get
            {
                List<ConeColour> list = new List<ConeColour>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From ConeColour").Rows)
                {
                    list.Add(new ConeColour(data));
                }
                return list;
            }
            private set {; }
        }

        public List<Lot> Lots
        {
            get
            {
                List<Lot> list = new List<Lot>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Lot").Rows)
                {
                    list.Add(new Lot(data));
                }
                return list;
            }
            private set {; }
        }

        public BaykonDbContext()
        {
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SmetaDbAppContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
