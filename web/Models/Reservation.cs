using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // checker za correct mail

namespace web.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public DateTime DateOfReservation { get; set; }  // date and time 

        public int Duration { get; set; } // kako dolgo mislmo bit v restavraciji

//tuji kluci 
        public int TableID { get; set; }
        public int GuestID { get; set; }

// povezave 
        public Guest Guest { get; set; } // povezava 1 na 1 na Guesta
        public Table Table { get; set; } // povezava 1 na 1 na Table 
    }
}