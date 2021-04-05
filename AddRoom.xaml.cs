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
    public partial class AddRoom : ContentPage
    {
        public MainPage mainPage;
        public AddRoom(MainPage main)
        {
            mainPage = main;
            InitializeComponent();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (ValidateUserInput())
            {


                Room newRoom = new Room();
                newRoom.RoomName = txtRoomTitle.Text;
                newRoom.Start = dpStartDate.Date;
                newRoom.End = dpEndDate.Date;
                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    con.Insert(newRoom);
                    mainPage.rooms.Add(newRoom);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await Navigation.PushModalAsync(new InputError());
            }

        }
        private bool ValidateUserInput()                     //Validates all fields are completed in dates are in correct format
        {
            bool valid = true;

            if (txtRoomTitle.Text == null ||
                dpStartDate.Date == null ||
                dpEndDate.Date == null ||
                dpEndDate.Date < dpStartDate.Date)

            {
                return false;
            }
            return valid;
        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}