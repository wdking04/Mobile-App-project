using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CapKing1.Classes
{
    [Table("Rooms")]
    public class Room
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string RoomName { get; set; }

        public string PatientName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


    }
}
