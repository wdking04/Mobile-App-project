using CapKing1.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapKing1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomPage : ContentPage
    {
        public Room _room;
        public MainPage _main;
        public Patient _patient;

        public RoomPage()
        {
            InitializeComponent();
        }

        public RoomPage(Room room,MainPage main)
        {
            _room = room;
            _main = main;
            InitializeComponent();
            patientListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(ItemTapped);
            Title = room.RoomName;


        }
        protected override void OnAppearing()
        {
            roomStart.Text = _room.Start.ToString("MM/dd/yyyy");
            roomEnd.Text = _room.End.ToString("MM/dd/yyyy");

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Patient>();
                var patientsForRoom = con.Query<Patient>($"SELECT * FROM Patients WHERE RoomName = '{_room.Id}'");
                patientListView.ItemsSource = patientsForRoom;
            }
        }

        async private void btnAddPatient_Clicked(object sender, EventArgs e)
        {
            // Only allow 2 patients per room
            if (getPatientCount() < 2)
            {
                await Navigation.PushModalAsync(new AddPatient(_room,_main));
            }
            else
            {
                // modal windows saying "can't add more than 2 patients"
                await Navigation.PushModalAsync(new PatientMaximum());
            } 
        }

        int getPatientCount()
        {
            int count = 0;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var patientCount = con.Query<Patient>($"SELECT * FROM Patients WHERE RoomName = '{_room.Id}'");
                count = patientCount.Count;
            }

            return count;
        }
        async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Patient patient = (Patient)e.Item;
            await Navigation.PushAsync(new PatientPage(_room,_main,patient));
        }

        private async void btnDeleteRoom_Clicked(object sender, EventArgs e)
        {
            
            var result = await this.DisplayAlert("Alert", "Confirm you wish to delete this room.", "Yes", "No");
            if(result)
            {
                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    var patients = con.Query<Patient>($"SELECT * FROM Patients WHERE RoomName = '{_room.Id}'");
                    foreach(Patient c in patients)
                    {
                        var treatments = con.Query<Treatment>($"SELECT * FROM Treatments WHERE PatientName = '{c.Id}'");
                        foreach(Treatment a in treatments)
                        {
                            con.Delete(a);
                        }
                        con.Delete(c);
                    }
                    con.Delete(_room);
                    //await Navigation.PopToRootAsync();
                    //await Navigation.PushAsync(new MainPage());
                      await Navigation.PopAsync();

                }
                    
            }
        }

        private async void btnEditRoom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditRoomPage(_room, _main, _patient));
        }




       // private async void btnBack_Clicked(object sender, EventArgs e)
       // {
        //    await Navigation.PopToRootAsync();
       // }
       
    }


}