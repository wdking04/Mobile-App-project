using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CapKing1.Classes
{
    [Table("Patients")]
    public class Patient
    {
        [PrimaryKey, AutoIncrement]
        
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientStatus { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string DoctorName { get; set; }
       // public string DoctorEmail { get; set; }
       // public string DoctorPhone { get; set; }
        public int RoomName { get; set; }
        public string Notes { get; set; }
        //public int Notifications { get; set; }
    }
}
