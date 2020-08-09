using BaykonIntegrationApp.Db;
using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BaykonIntegrationApp.Models
{
    public class PackageView : AbstractModel
    {
        private string value;
        public string Value
        {
            get { return value; }
            set { this.value = value; OnPropertyChanged("Value"); }
        }

        private double weight;
        public double Weight
        {
            get { return weight; }
            set { weight = value; OnPropertyChanged("Weight");}
        }


        /// <summary>
        /// Type == 1  bag, Type = 0 box
        /// </summary>
        public int Type { get; set; }

        public PackageView()
        {
            Value = "";
            Type = 1;
        }

        public PackageView(string Name, string Value = "", double Weight = 2, int Type = 1)
        {
            this.Name = Name;
            this.Value = Value;
            this.Weight = Weight;
            this.Type = Type;
        }

        public PackageView(DataRow data)
        {
            Id = long.Parse(data.ItemArray[0].ToString());
            Name = data.ItemArray[1].ToString();
            Value = data.ItemArray[2].ToString();
            Weight = double.Parse(data.ItemArray[3].ToString());
            Type = int.Parse(data.ItemArray[4].ToString());
        }

        #region Data base actions
        public override void Save()
        {
            string query = $"Insert Into PackageView " +
                "(Name, Value, Weight, Type) Values (" +
                $"'{Name}', '{Value}', {Weight.ToString().Replace(',', '.')}, {Type})";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update PackageView Set " +
                $"Name = '{Name}', Value = '{Value}', Weight = {Weight.ToString().Replace(',', '.')}, Type = {Type} Where Id = {Id}";
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = $"Delete From PackageView Where Id = {Id}";
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
