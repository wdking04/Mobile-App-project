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
    public partial class AddTreatment : ContentPage
    {
        public Patient _patient;
        public MainPage _main;
        public AddTreatment(Patient patient, MainPage main)
        {
            _patient = patient;
            _main = main;
            InitializeComponent();
        }

       

        async private void btnAddTreatment_Clicked(object sender, EventArgs e)
        {
            if (ValidateUserInput())
            {
                Treatment newTreatment = new Treatment();
                newTreatment.TreatmentName = txtTreatmentName.Text;
                newTreatment.TreatmentType = pickerTreatmentType.SelectedItem.ToString();
                newTreatment.PatientName = _patient.Id;

                using (SQLiteConnection con = new SQLiteConnection(App.FilePath))
                {
                    var manualCount = con.Query<Treatment>($"SELECT * FROM Treatments WHERE PatientName = '{_patient.Id}' AND TreatmentType = 'Manual'");
                    var therexCount = con.Query<Treatment>($"SELECT * FROM Treatments WHERE PatientName = '{_patient.Id}' AND TreatmentType = 'TherEx'");
                    if (newTreatment.TreatmentType.ToString() == "Manual" && manualCount.Count == 0)
                    {
                        con.Insert(newTreatment);
                        _main.treatments.Add(newTreatment);
                        await Navigation.PopModalAsync();
                    }
                    else if (newTreatment.TreatmentType.ToString() == "TherEx" && therexCount.Count == 0)
                    {
                        con.Insert(newTreatment);
                        _main.treatments.Add(newTreatment);
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await Navigation.PushModalAsync(new TreatmentError());
                    }
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

            if (txtTreatmentName == null ||
                pickerTreatmentType.SelectedItem == null 
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