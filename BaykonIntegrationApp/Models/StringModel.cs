using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaykonIntegrationApp.Models
{
    public class StringModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static bool operator == (StringModel ob1, StringModel ob2)
        {
            return ob1.Id == ob2.Id;
        }

        public static bool operator !=(StringModel ob1, StringModel ob2)
        {
            return ob1.Id != ob2.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return base.Equals(obj);
        }

        public StringModel Copy()
        {
            return new StringModel()
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        //public static void operator =(string Name)
        //{
        //    this.Name = Name;
        //}

    }
}
