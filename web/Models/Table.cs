using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // checker za correct mail

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace web.Models
{
    public class Table
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // to more bit da lahk sam lahka unesel
        public int TableID { get; set; }

        public int MinSize { get; set; }  // velikost mize za min stevilo gostov

        public int MaxSize { get; set; }  // velikost mize za max stevilo gostov

        [Range(0, 1)] // torej 0 ali 1
        public int Online { get; set; } // ali je miza na voljo 1, miza ni navoljo 0

        public int RestaurantID { get; set; } // tuj kluc od Restauranta

        public Restaurant Restaurant { get; set; } // povezava 1 na 1 na Restaurant ker pripada doloceni restavraciji
        public Reservation Reservation { get; set; } // povezava 1 na 0 na Reservation  

    }
}