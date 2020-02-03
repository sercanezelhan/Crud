using Crud.Enums;
using Crud.Helper;
using Crud.Model;
using Crud.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Crud.ViewModel
{
    public class AddWindowViewModel : INotifyPropertyChanged
    {
       
        #region Members 

        /// <summary>
        /// Model nesnesi
        /// </summary>
        private Person person;

        /// <summary>
        /// AddView nesnesi view pencere kontrolü
        /// </summary>
        private AddWindow AddWindow { get; set; }

        /// <summary>
        /// AddView Image Nesnesi
        /// </summary>
        private string imageName { get; set; }

        /// <summary>
        /// Image Base64
        /// </summary>
        private string base64String { get; set; }

        #endregion

        #region Constructors

        public AddWindowViewModel(AddWindow a)
        {
            this.AddWindow = a;
            Person = new Person();
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

        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                OnPropertyChanged("ImageName");
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// PersonList ekleme events
        /// </summary>
        public event EventHandler AddEvent;

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

        public ICommand AddImage
        {
            get
            {
                if (addImage == null)
                    addImage = new RelayCommand(ImageDialog);
                return addImage;
            }
        }

        #endregion

        #region ICommands

        /// <summary>
        /// AddView Add butonu
        /// </summary>
        private ICommand addPersonCommand;

        /// <summary>
        /// AddView Image Butonu
        /// </summary>
        private ICommand addImage;

        #endregion

        #region Methods
        /// <summary>
        /// AddView Add butonu, add metodu
        /// </summary>
        public void AddPerson()
        {
            if (!string.IsNullOrEmpty( Person.FirstName) && !string.IsNullOrEmpty(Person.LastName) && Person.Gender != null && !string.IsNullOrEmpty(ImageName))
            {
                Person p = new Person()
                {
                    FirstName = Person.FirstName,
                    LastName = Person.LastName,
                    Age = Person.Age,
                    Gender =  Person.Gender,
                    PersonImage=base64String
                };
                AddEvent?.Invoke(p, null);
                Person = new Person();
                ImageName = string.Empty;
                MessageBoxResult result = MessageBox.Show("Çıkmak istiyor musunuz?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AddWindow.Close();
                }
            }
            else
                MessageBox.Show("Alanları boş bırakamazsınız");
        }

        /// <summary>
        /// OpenFileDialog Image to Base64 Metodu
        /// </summary>
        private void ImageDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files |*.*|JPG File|*.jpg,*.jpeg|PNG File|*.png";
            openFileDialog.Title = "Image";
            openFileDialog.ShowDialog();
            ImageName = openFileDialog.FileName;
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog.FileName))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }
        }

        public void OnPropertyChanged(string update)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(update));
        }

        #endregion
    }
}
