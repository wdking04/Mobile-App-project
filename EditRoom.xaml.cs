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
    public partial class EditRoomPage : ContentPage
    {
        public Room _room;
        public Patient _patient;
        public MainPage _main;
        public EditRoomPage(Room room, MainPage main, Patient patient)
        {
            _room = room;
            _patient = patient;
            _main = main;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            txtRoomName.Text = _room.RoomName;
            dpStartDate.Date = _room.Start.Date;
            dpEndDate.Date = _room.End.Date;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if(ValidateUserInput())
            {

                _room.RoomName = txtRoomName.Text;
                _room.Start = dpStartDate.Date;
                _room.End = dpEndDate.Date;
                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    con.Update(_room);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await Navigation.PushModalAsync(new InputError());
            }

        }
        private bool ValidateUserInput()
        {
            bool valid = true;

            if (txtRoomName.Text == null ||
                dpStartDate.Date == null ||
                dpEndDate.Date == null ||
                dpEndDate.Date < dpStartDate.Date
                )

            {
                return false;
            }
            return valid;
        }

       // private void btnExit_Clicked(object sender, EventArgs e)
       // {
           // Navigation.PopAsync();
            
        //}
    }
}