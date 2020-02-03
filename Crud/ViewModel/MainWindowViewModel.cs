using Crud.Helper;
using Crud.Model;
using Crud.View;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Crud.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
       
        #region Members

        /// <summary>
        /// Model nesnesi 
        /// </summary>
        private Person person;

        /// <summary>
        /// View selection person
        /// </summary>
        private Person selectedPerson;

        /// <summary>
        /// AddPerson ID
        /// </summary>
        private int personID { get; set; }

        /// <summary>
        /// Database işlemleri sınıfı.
        /// </summary>
        private DataBase dataBase { get; set; }

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            Person = new Person();
            dataBase = new DataBase();
            PersonList = new ObservableCollection<Person>(dataBase.DataBaseQuery());
        }

        #endregion

        #region Properties

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        public Person Person
        {
            get { return person; }
            set
            {
                person = value;
                OnPropertyChanged("Person");
            }
        }

        public ObservableCollection<Person> PersonList { get; set; }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand AddPersonCommand
        {
            get
            {
                if (addPersonCommand == null)
                    addPersonCommand = new RelayCommand(AddPerson);
                return addPersonCommand;
            }
        }
        public ICommand UpdatePersonCommand
        {
            get
            {
                if (updatePersonCommand == null)
                    updatePersonCommand = new RelayCommand<Person>(UpdatePerson);
                return updatePersonCommand;
            }
        }
        public ICommand DeletePersonCommand
        {
            get
            {
                if (deletePersonCommand == null)
                    deletePersonCommand = new RelayCommand(DeleteSelectionPerson);
                return deletePersonCommand;
            }
        }
        public ICommand DeletePersonListViewButtonCommand
        {
            get
            {
                if (deletePersonListViewButtonCommand == null)
                    deletePersonListViewButtonCommand = new RelayCommand<Person>(DeletePerson);
                return deletePersonListViewButtonCommand;
            }
        }

        #endregion

        #region ICommands

        /// <summary>
        /// View add butonu
        /// </summary>
        private ICommand addPersonCommand;

        /// <summary>
        /// View person update butonu
        /// </summary>
        private ICommand updatePersonCommand;

        /// <summary>
        /// View selection person delete butonu
        /// </summary>
        private ICommand deletePersonCommand;

        /// <summary>
        /// View person delete butonu
        /// </summary>
        private ICommand deletePersonListViewButtonCommand;

        #endregion

        #region Methods

        /// <summary>
        /// View add butonu, add metodu
        /// </summary>
        public void AddPerson()
        {
            using (AddWindow a = new AddWindow())
            {
                ((AddWindowViewModel)a.DataContext).AddEvent += MainWindowViewModel_AddEvent;
                a.ShowDialog();
                a.Dispose();
            }
        }

        /// <summary>
        /// View person update butonu, Person update metodu
        /// </summary>
        /// <param name="p"></param>
        public void UpdatePerson(Person p)
        {
            using (UpdateWindow u = new UpdateWindow(p))
            {
                ((UpdateWindowViewModel)u.DataContext).UpdateEvent += MainWindowViewModel_UpdateEvent;
                u.ShowDialog();
                u.Dispose();
            }
        }

        /// <summary>
        /// View add butonu, add event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowViewModel_AddEvent(object sender, EventArgs e)
        {
            Person person = (Person)sender;
            PersonList.Add(dataBase.DataBaseAdd(person));
            MessageBox.Show($"{person.ToString()} \nKişinin Kaydı Başarıyla Gerçekleştirilmştir.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Person update butonu, update event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowViewModel_UpdateEvent(object sender, EventArgs e)
        {
            for (int i = 0; i < PersonList.Count; i++)
                if (PersonList[i].ID == ((Person)sender).ID)
                {
                    Person p1 = ((Person)sender);
                    Person p = new Person
                    {
                        FirstName = p1.FirstName,
                        LastName = p1.LastName,
                        Age = p1.Age,
                        Gender = p1.Gender,
                        ID=p1.ID
                    };
                    PersonList[i] = dataBase.DataBaseUpdate(p);
                    MessageBox.Show($"Bilgi/Bilgiler Başarıyla Güncellendi.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
        }

        /// <summary>
        /// View selection person delete butonu, delete metodu
        /// </summary>
        public void DeleteSelectionPerson()
        {
            if (SelectedPerson != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"{SelectedPerson.ToString()} \nKişiyi Silmek İstediğinizden Emin misiniz?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                if (messageBoxResult.ToString().ToLower() == "Yes".ToLower())
                {
                    PersonList.Remove(dataBase.DataBaseDelete(SelectedPerson));
                    MessageBox.Show("Kayıt Başarıyla Silinmiştir.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                    MessageBox.Show("Silme işlemi iptal edilmiştir.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("Lütfen Eleman Seçiniz");
        }

        /// <summary>
        /// View person delete butonu, Person delete metodu
        /// </summary>
        /// <param name="p"></param>
        public void DeletePerson(Person person)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"{person.ToString()} \nKişiyi Silmek İstediğinizden Emin misiniz?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            if (messageBoxResult.ToString().ToLower() == "Yes".ToLower())
            {
                PersonList.Remove(dataBase.DataBaseDelete(person));
                MessageBox.Show($"Kayıt Başarıyla Silinmiştir.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("Silme işlemi iptal edilmiştir.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Person List
        /// </summary>
        public void OnPropertyChanged(string update)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(update));
        }

        #endregion

    }
}
