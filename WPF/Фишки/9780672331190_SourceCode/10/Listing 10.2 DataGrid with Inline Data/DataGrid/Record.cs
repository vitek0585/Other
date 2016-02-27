using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrid
{
    public class Record
    {
        public string NamePlan { get; set; }

        public Record()
        {
            
        } 
        public Record(string name)
        {
            NamePlan = name;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri Website { get; set; }
        public bool IsBillionaire { get; set; }
        public Gender Gender { get; set; }

        public object Show(Type g)
        {
            return "Hello "+g.ToString();
        }
        public string Show(double g)
        {
            return NamePlan  +g;
        }
        //public IEnumerable Show(Type type)
        //{
        //    yield break;
        //}
    }

    public enum Gender
    {
        Male,
        Female
    }
}