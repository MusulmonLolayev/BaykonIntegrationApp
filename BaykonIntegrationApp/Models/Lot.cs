using BaykonIntegrationApp.Db;
using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaykonIntegrationApp.Models
{
    public class Lot : AbstractModel
    {
        public double Val { get; set; }

        private int lastNumber;
        public int LastNumber
        {
            get { return lastNumber; }
            set { lastNumber = value; OnPropertyChanged("LastNumber"); }
        }

        public Lot()
        {
            Val = 0;
        }

        public Lot(string Name, double Val = 0, int LastNumber = 2)
        {
            this.Name = Name;
            this.Val = Val;
            this.LastNumber = LastNumber;
        }

        public Lot(DataRow data)
        {
            Id = long.Parse(data.ItemArray[0].ToString());
            Name = data.ItemArray[1].ToString();
            //Val = double.Parse(data.ItemArray[2].ToString());
            LastNumber = int.Parse(data.ItemArray[3].ToString());
        }

        #region Data base actions
        public override void Save()
        {
            string query = $"Insert Into Lot " +
                "(Name, Val, LastNumber) Values (" +
                $"'{Name}', {Val}, {LastNumber})";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update Lot Set " +
                $"Name = '{Name}', Val = {Val}, LastNumber = {LastNumber} Where Id = {Id}";
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = $"Delete From Lot Where Id = {Id}";
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
