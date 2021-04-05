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
    public partial class EditTreatmentPage : ContentPage
    {
        public Patient _patient;
        public Treatment _treatment;
        public MainPage _main;
        public List<string> pickerStates = new List<string> { "Manual", "TherEx" };
        public List<string> pickerNotificationsStates = new List<string> { "Yes", "No" };
        public EditTreatmentPage(Patient patient, MainPage main, Treatment treatment)
        {
            _patient = patient;
            _treatment = treatment;
            _main = main;
            InitializeComponent();
        }
      

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnSaveChanges_Clicked(object sender, EventArgs e)
        {
            bool changedTreatmentType = false;
            if(_treatment.TreatmentType.ToString() != pickerTreatmentType.SelectedItem.ToString())
            {
                changedTreatmentType = true;
            }
            _treatment.TreatmentName = txtTreatmentName.Text;

            using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
            {
                var manualCount = con.Query<Treatment>($"SELECT * FROM Treatments WHERE PatientName = '{_patient.Id}' AND TreatmentType = 'Objective'");
                var therexCount = con.Query<Treatment>($"SELECT * FROM Treatments WHERE PatientName = '{_patient.Id}' AND TreatmentType = 'Performance'");
                if (_treatment.TreatmentType.ToString() == "Manual" && manualCount.Count == 0)
                {
                    _treatment.TreatmentType = pickerTreatmentType.SelectedItem.ToString();
                    con.Update(_treatment);
                    await Navigation.PopAsync();
                }
                else if (_treatment.TreatmentType.ToString() == "TherEx" && therexCount.Count == 0)
                {
                    _treatment.TreatmentType = pickerTreatmentType.SelectedItem.ToString();
                    con.Update(_treatment);
                    await Navigation.PopAsync();
                }
                else if (((_treatment.TreatmentType.ToString() == "TherEx" && therexCount.Count == 1) ||
                         (_treatment.TreatmentType.ToString() == "Manual" && manualCount.Count == 1)) &&
                         !(String.IsNullOrEmpty(_treatment.Id.ToString())) && 
                          !changedTreatmentType)
                {
                    con.Update(_treatment);
                    await Navigation.PopAsync();
                }
                else
                {
                    await Navigation.PushModalAsync(new TreatmentError());
                }
            }


        }

        private async void btnDeleteTreatment_Clicked(object sender, EventArgs e)
        {
            // Delete an treatment


            var result = await this.DisplayAlert("Alert!", "Confirm you wish to delete this treatment?", "Yes", "No");
            if (result)
            {
                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    con.Delete(_treatment);


                    //await Navigation.PopToRootAsync();
                    await Navigation.PopAsync();
                }

            }
        }
        private void btnMain_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}