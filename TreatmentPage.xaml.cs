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
    public partial class TreatmentPage : ContentPage
    {
        public Patient _patient;
        public MainPage _main;

        public TreatmentPage(Patient patient, MainPage main)
        {
            _patient = patient;
            _main = main;
            InitializeComponent();
            TreatmentListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(ItemTapped);
            Title = patient.PatientName;
        }

        protected override void OnAppearing()
        {
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                con.CreateTable<Treatment>();
                var treatmentsForCourse = con.Query<Treatment>($"SELECT * FROM Treatments WHERE PatientName = '{_patient.Id}'");
                TreatmentListView.ItemsSource = treatmentsForCourse;
            }
        }


        async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Treatment treatment = (Treatment)e.Item;
            await Navigation.PushAsync(new EditTreatmentPage(_patient, _main, treatment));
        }

        async private void btnAddAssessment_Clicked(object sender, EventArgs e)
        {
           
             if (getTreatmentCount() < 2)     // 2 treatments per patient
            {
                await Navigation.PushModalAsync(new AddTreatment(_patient,_main));
            }

            else
            {
                await Navigation.PushModalAsync(new TreatmentError());        //treatment error screen if more than 2 are added
            }
        }

        int getTreatmentCount()
        {
            int count = 0;
            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var assessmentCount = con.Query<Treatment>($"SELECT * FROM Treatments WHERE PatientName = '{_patient.Id}'");
                count = assessmentCount.Count;
            }

            return count;
        }
        //private void btnBack_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PopAsync();
        //}
    }
}