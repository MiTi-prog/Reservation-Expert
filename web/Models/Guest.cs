using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // checker za correct mail

namespace web.Models
{
    public class Guest
    {
        public int GuestID { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "First Name Required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }
        //druga opcija z uporabo regexa
        //[Required(ErrorMessage="Email is required")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+[A-Za-z0-9.-]+\.[A-Za-z] {2,4}",
        //public String Email {get; set;}

        [Required(ErrorMessage="Mobile Phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Entered phone format is not valid.")]
        //012345678, 012-345-678, (012)-345-678, (012)345678 012 345678, 012 345 678, 012 345-678, (012) 345-678, 012.345.678 opcije kako lahko zapisemo telefonsko
        public string MobileNumber {get; set;}
    
// ni nobenih tujih kljucev ne nc ne tak da je tuki vse kul sam to povezavo mamo 
        public ICollection<Reservation> Reservations { get; set; } // povezava 1 to N na Rezervacijo bi delat tut 0 to N ker gre za foro z podatkovnimi stvarmi 
    }
}