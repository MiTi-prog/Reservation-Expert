using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // checker za correct mail

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace web.Models
{
    public class Restaurant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // za generatanje
        public int RestaurantID { get; set; }
        
        public string NameOfRestaurant { get; set; }
   
        public string Location { get; set; }

        public int TableCapacity { get; set; }

        [Required(ErrorMessage="Mobile Phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Entered phone format is not valid.")]
        //012345678, 012-345-678, (012)-345-678, (012)345678 012 345678, 012 345 678, 012 345-678, (012) 345-678, 012.345.678 opcije kako lahko zapisemo telefonsko
        public string MobileNumber {get; set;}

        public int Open { get; set; }  // cas kdaj se odpre 8 kr v intih polne ure

        public int Close { get; set; } // cas kdaj se zapre nevem 22 v intih polne ure

// ni nobenih tujih kljucev ne nc ne tak da je tuki vse kul sam to povezavo mamo     
        public ICollection<Table> Tables { get; set; } // povezava 1 na N na Tablese 

    }
}