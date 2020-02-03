using Crud.Helper;
using Crud.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Crud.ViewModel
{
    public class UpdateWindowViewModel
    {

 
        #region Members

        /// <summary>
        /// Model nesnesi
        /// </summary>
        private Person person;

        #endregion

        #region Constructors

        public UpdateWindowViewModel(Person _person)
        {
            Person p = new Person()
            {
                FirstName = _person.FirstName,
                LastName = _person.LastName,
                Age = _person.Age,
                Gender = _person.Gender,
                ID = _person.ID
            };
            Person = p;
        }

        #endregion

        #region Properties

        public Person Person
        {
            get { return person; }
            set
            {
                person = value;
                OnPropertyChanged("Person");
            }
        }

        #endregion

        #region Events

        public event EventHandler UpdateEvent;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Commands

        public ICommand UpdatePersonCommand
        {
            get
            {
                if (updatePersonCommand == null)
                    updatePersonCommand = new RelayCommand(Update);
                return updatePersonCommand;
            }
        }

        #endregion

        #region ICommands

        /// <summary>
        /// UpdateView Update butonu
        /// </summary>
        private ICommand updatePersonCommand;

        #endregion

        #region Methods

        /// <summary>
        /// UpdateView Update butonu, Update metodu
        /// </summary>
        public void Update()
        {
            if (Person.FirstName != string.Empty && Person.LastName != string.Empty)
            {
                Person p = new Person()
                {
                    FirstName = Person.FirstName,
                    LastName = Person.LastName,
                    Age = Person.Age,
                    Gender = Person.Gender,
                    ID = Person.ID
                };
                UpdateEvent?.Invoke(p, null);
            }
            else
                MessageBox.Show("Alanları boş bırakamazsınız");
        }

        public void OnPropertyChanged(string update)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(update));
        }

        #endregion
    }
}
