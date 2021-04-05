using CapKing1.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapKing1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPatient : ContentPage
    {
        public Room roomPage;
        public MainPage mainPage;
        Dictionary<string, bool> notificationsDict = new Dictionary<string, bool>
        {
            {"Yes",true},
            {"No",false}
        };
        public AddPatient(Room room,MainPage main)
        {
            roomPage = room;
            mainPage = main;
            InitializeComponent();
        }

        private async void btnSavePatient_Clicked(object sender, EventArgs e)
        {
            if (ValidateUserInput())
            {

                Patient newPatient = new Patient();
                newPatient.PatientName = txtPatientTitle.Text;
                newPatient.PatientStatus = pickerPatientStatus.SelectedItem.ToString();   
                newPatient.DoctorName = txtDoctorName.Text;
                newPatient.Notes = txtNotes.Text;
                newPatient.RoomName = roomPage.Id;



                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    con.Insert(newPatient);

                    mainPage.patients.Add(newPatient);
                    
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await Navigation.PushModalAsync(new InputError());
            }

        }

       

        private bool ValidateUserInput()                   //Validates all fields have info added and in correct format
        {
            bool valid = true;

            if (txtPatientTitle == null ||
                pickerPatientStatus.SelectedItem == null ||   
                txtDoctorName == null 
                )
            
            {
                return false;
            }
            
            

            return valid;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }

}