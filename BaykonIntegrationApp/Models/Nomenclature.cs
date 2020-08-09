﻿using BaykonIntegrationApp.Db;
using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaykonIntegrationApp.Models
{
    public class Nomenclature : AbstractModel
    {
        public double Val { get; set; }

        public Nomenclature()
        {
            Val = 0;
        }

        public Nomenclature(string Name, double Val = 0)
        {
            this.Name = Name;
            this.Val = Val;
        }

        public Nomenclature(DataRow data)
        {
            Id = long.Parse(data.ItemArray[0].ToString());
            Name = data.ItemArray[1].ToString();
        }

        #region Data base actions
        public override void Save()
        {
            string query = $"Insert Into Nomenclature " +
                "(Name, Val) Values (" +
                $"'{Name}', {Val})";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update Nomenclature Set " +
                $"Name = '{Name}', Val = {Val} Where Id = {Id}";
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = $"Delete From Nomenclature Where Id = {Id}";
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
