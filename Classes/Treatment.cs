using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CapKing1.Classes
{
    [Table("Treatments")]
    public class Treatment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TreatmentName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string TreatmentType { get; set; }
        public int PatientName { get; set; }
        //public int Notifications { get; set; }

    }
}
