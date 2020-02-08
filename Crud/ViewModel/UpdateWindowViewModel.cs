using Crud.Helper;
using Crud.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Crud.ViewModel
{
    public class UpdateWindowViewModel
    {













        #region Members

        /// <summary>
        /// Model nesnesi
        /// </summary>
        private Person person;

        /// <summary>
        /// UpdateView Image Nesnesi
        /// </summary>
        private BitmapFrame imageName;

        #endregion

        #region Constructors

        public UpdateWindowViewModel(Person person)
        {
            Person p = new Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                Gender = person.Gender,
                ID = person.ID,
                PersonImage=person.PersonImage
            };
            Person = p;
            ImageName = ImageConvert(p.PersonImage);
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

        public BitmapFrame ImageName
        {
            get{ return imageName; }
            set
            {
                imageName = value;
                OnPropertyChanged("ImageName");
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

        public ICommand UpdateImageCommand
        {
            get
            {
                if (updateImageCommand == null)
                    updateImageCommand = new RelayCommand(ImageDialog);
                return updateImageCommand;
            }
        }

        #endregion

        #region ICommands

        /// <summary>
        /// UpdateView Update butonu
        /// </summary>
        private ICommand updatePersonCommand;

        /// <summary>
        /// UpdateView Image Butonu
        /// </summary>
        private ICommand updateImageCommand;

        #endregion

        #region Methods

        /// <summary>
        /// UpdateView Update butonu, Update metodu
        /// </summary>
        public void Update()
        {
            if (!string.IsNullOrEmpty(Person.FirstName) && !string.IsNullOrEmpty(Person.LastName) && Person.Gender != null && Person.PersonImage!=null)
            {
                Person p = new Person()
                {
                    FirstName = Person.FirstName,
                    LastName = Person.LastName,
                    Age = Person.Age,
                    Gender = Person.Gender,
                    ID=Person.ID,
                    PersonImage = base64String
                };
                UpdateEvent?.Invoke(p, null);
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
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog.FileName))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    BitmapFrame bitmapFrame = ImageConvert(base64String);
                    ImageName = bitmapFrame;
                }
            }
        }
        
        private string base64String { get;set; }
        
        /// <summary>
        /// Base64 to fotoğraf metodu.
        /// </summary>
        /// <returns></returns>
        private BitmapFrame ImageConvert(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return null;
            var imgBytes = System.Convert.FromBase64String(base64);
            if (imgBytes == null)
                return null;
            using (var stream = new MemoryStream(imgBytes))
            {
                return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }

        public void OnPropertyChanged(string update)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(update));
        }

        #endregion
    }
}
