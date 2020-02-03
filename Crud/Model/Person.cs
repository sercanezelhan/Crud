using Crud.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Model
{
    public class Person
    {

        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public PersonGender? Gender { get; set; }

        public int ID { get; set; }

        public string PersonImage { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// ToString ezici metot.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Ad : {this.FirstName} \nSoyad : {this.LastName} \nYaş : {this.Age} \nCinsiyet : {this.Gender} ";
        }

        #endregion

    }
}
