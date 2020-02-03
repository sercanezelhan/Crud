using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Crud.Model;
using Crud.Enums;
using System.IO;

namespace Crud.Helper
{
    public class DataBase
    {

        
        #region Members

        /// <summary>
        /// SQLite bağlantı adresi.
        /// </summary>
        private string sqlitedbConstr { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public DataBase()
        {
            sqlitedbConstr = @"D:\Uygulamalar\Visual Studio Uygulamalar\Crud\Crud\Person.db";
            if (!File.Exists(sqlitedbConstr))
            {
                sqlitedbConstr = @"Data source = D:\Uygulamalar\Visual Studio Uygulamalar\Crud\Crud\Person.db;Version=3;";
                using (var connection = new SQLiteConnection(sqlitedbConstr))
                {
                    connection.Open();
                    using (var sqliteCommand = new SQLiteCommand("CREATE TABLE Person (FirstName TEXT,LastName  TEXT,Age INTEGER,Gender INTEGER,ID INTEGER PRIMARY KEY AUTOINCREMENT,Image TEXT)", connection))
                    {
                        sqliteCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            else
                sqlitedbConstr = @"Data source = D:\Uygulamalar\Visual Studio Uygulamalar\Crud\Crud\Person.db;Version=3;";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Veritabanı ekleme işlemi.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Person DataBaseAdd(Person person)
        {
            Person p = new Person 
            { 
                FirstName = person.FirstName.ToUpper(),
                LastName = person.LastName.ToUpper(), 
                Age = person.Age, 
                Gender = person.Gender, 
                ID = person.ID ,
                PersonImage=person.PersonImage
            };
            using (var connection = new SQLiteConnection(sqlitedbConstr))
            {
                using (var sqliteCommand = new SQLiteCommand($"INSERT INTO Person VALUES ('{p.FirstName}','{p.LastName}',{p.Age},'{(int)p.Gender}',null,'{p.PersonImage}')", connection))
                {
                    sqliteCommand.Connection.Open();
                    sqliteCommand.ExecuteNonQueryAsync();
                    sqliteCommand.CommandText = "SELECT id FROM Person ORDER BY id DESC";
                    person.ID =Convert.ToInt32(sqliteCommand.ExecuteScalar());
                }
                return p;
            }
        }

        /// <summary>
        /// Veritabanı güncelleme işlemi.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
       public Person DataBaseUpdate(Person person)
        {
            Person p = new Person
            {
                FirstName = person.FirstName.ToUpper(),
                LastName = person.LastName.ToUpper(),
                Age = person.Age,
                Gender = person.Gender,
                ID = person.ID,
                PersonImage=person.PersonImage
            };
            using (var connection = new SQLiteConnection(sqlitedbConstr))
            {
                using (var sqliteCommand = new SQLiteCommand($"UPDATE Person SET FirstName='{p.FirstName}',LastName='{p.LastName}',Age={p.Age},Gender='{(int)p.Gender}',Image='{p.PersonImage}' WHERE ID={p.ID}", connection))
                {
                    sqliteCommand.Connection.Open();
                    sqliteCommand.ExecuteNonQueryAsync();
                }
                return p;
            }
        }

        /// <summary>
        /// Veritabanı silme işlemi.
        /// </summary>
        /// <param name="person"></param>
        public Person DataBaseDelete(Person person)
        {
            using (var connection = new SQLiteConnection(sqlitedbConstr))
            {
                using (var sqliteCommand = new SQLiteCommand($"DELETE FROM Person WHERE ID={person.ID}", connection))
                {
                    sqliteCommand.Connection.Open();
                    sqliteCommand.ExecuteNonQuery();
                }
                return person;
            }
        }

        /// <summary>
        /// Veritabanı güncelleme işlemi.
        /// </summary>
        /// <returns></returns>
        public List<Person> DataBaseQuery()
        {
            List<Person> personList = new List<Person>  ();
            using (var connection = new SQLiteConnection(sqlitedbConstr))
            {
                using (var sqliteCommandquery = new SQLiteCommand("SELECT * FROM Person", connection))
                {
                    sqliteCommandquery.Connection.Open();
                    SQLiteDataReader sQLiteDataReader = sqliteCommandquery.ExecuteReader();
                    while (sQLiteDataReader.Read())
                        personList.Add(new Person
                        {
                            FirstName = sQLiteDataReader["FirstName"].ToString(),
                            LastName = sQLiteDataReader["LastName"].ToString(),
                            Age = Convert.ToInt32(sQLiteDataReader["Age"]),
                            Gender = (PersonGender)Convert.ToInt32(sQLiteDataReader["Gender"]),
                            ID = Convert.ToInt32(sQLiteDataReader["ID"]),
                            PersonImage=sQLiteDataReader["Image"].ToString()
                        });
                    sqliteCommandquery.ExecuteNonQueryAsync();
                }
            }
            return personList;
        }
        #endregion

    }
}
