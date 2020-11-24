using web.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ResExpertContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Guests.
            if (context.Restaurants.Any())
            {
                return;   // DB has been seeded
            }

// Gosti -- oz to bi rad mel nekak not kot userje  
            var guests = new Guest[]
            {
            new Guest{FirstName="Tim",LastName="Rus",Email="tim@fri.si", MobileNumber="031342123"},  // 1
            new Guest{FirstName="Mitja",LastName="Sepec",Email="mitja@fri.si", MobileNumber="041342123"}, // 2
            new Guest{FirstName="Meredith",LastName="Alonso",Email="meredith@fri.si", MobileNumber="051372283"}, //3
            new Guest{FirstName="Yan",LastName="Li",Email="yan@fri.si", MobileNumber="030729234"}, //4
            new Guest{FirstName="Laura",LastName="Norman",Email="laura@fri.si", MobileNumber="040543745"} //5
            //EnrollmentDate=DateTime.Parse("2005-09-01")
            };
            foreach (Guest g in guests)
            {
                context.Guests.Add(g);
            }
            context.SaveChanges();


            //DateOfReservation=DateTime.Parse("2020-12-01"),Duration=2, }, // yyyy-mm-dd

// Restavracije
            var restaurants = new Restaurant[]
            {
            new Restaurant{RestaurantID=1,NameOfRestaurant="Restavracija 123", Location="Vecna pot 13, 1000 Ljublana",TableCapacity=3, MobileNumber="031987567",Open=8,Close=14},
            new Restaurant{RestaurantID=2,NameOfRestaurant="Fontana", Location="Dalmatinova ulica 2, 8270 Krško",TableCapacity=2, MobileNumber="051987567",Open=8,Close=23},
            new Restaurant{RestaurantID=3,NameOfRestaurant="Oštarija Margareta", Location="cesta julija 2, 8270 Krško",TableCapacity=2, MobileNumber="030987567",Open=9,Close=22},
            new Restaurant{RestaurantID=4,NameOfRestaurant="Foculus", Location="Gregorčičeva ulica 3, 1000 Ljubljana",TableCapacity=4, MobileNumber="051987567",Open=9,Close=23}
            };
            foreach (Restaurant r in restaurants)
            {
                context.Restaurants.Add(r);
            }
            context.SaveChanges();

// Table
            var tables = new Table[]
            {
            new Table{TableID=11,MinSize=1,MaxSize=3,Online=1,RestaurantID=1}, // 0011 pomen od restavracije ena, miza ena
            new Table{TableID=21,MinSize=1,MaxSize=2,Online=1,RestaurantID=1},
            new Table{TableID=31,MinSize=3,MaxSize=5,Online=1,RestaurantID=1},

            new Table{TableID=12,MinSize=1,MaxSize=3,Online=1,RestaurantID=2}, // 0012 pomen od restavracije dva, miza ena
            new Table{TableID=22,MinSize=1,MaxSize=2,Online=1,RestaurantID=2},
          
            new Table{TableID=13,MinSize=1,MaxSize=3,Online=1,RestaurantID=3}, // 0013 pomen od restavracije tri, miza ena
            new Table{TableID=23,MinSize=1,MaxSize=2,Online=1,RestaurantID=3},

            new Table{TableID=14,MinSize=1,MaxSize=3,Online=1,RestaurantID=4}, // 0014 pomen od restavracije stiri, miza ena
            new Table{TableID=24,MinSize=1,MaxSize=2,Online=1,RestaurantID=4},
            new Table{TableID=34,MinSize=1,MaxSize=2,Online=1,RestaurantID=4},
            new Table{TableID=44,MinSize=1,MaxSize=2,Online=1,RestaurantID=4}
            };
            
            foreach (Table e in tables)
            {
                context.Tables.Add(e);
            }
            context.SaveChanges();


// Rezervacije
            string s1 = "2020-12-21 13:26";
            string s2 = "2020-12-18 12:23";

            var reservations = new Reservation[]
            {
                                                           // year, month, day, hour, min
            new Reservation{DateOfReservation = DateTime.Parse(s1),Duration=2,GuestID=1,TableID=12},  
            new Reservation{DateOfReservation = DateTime.Parse(s2),Duration=1,GuestID=2,TableID=23},
        
            };

            foreach (Reservation s in reservations)
            {
                context.Reservations.Add(s);
            }
            context.SaveChanges();


//user1 
            var user1 = new ApplicationUser  // dodamo novega userja not pa 
            {
                FirstName = "Mitja",
                LastName = "Sepec",
                Email = "mitja@fri.si",
                NormalizedEmail = "XXXXdddddd@fri.si",
                UserName = "mitja@fri.si",
                NormalizedUserName = "mitja@fri.si",
                MobileNumber = "041123456",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user1,"Frijekul123$"); // geslo Hassed geslo 
                user1.PasswordHash = hashed;
                context.Users.Add(user1);
                
            }

            context.SaveChanges(); // shranim spremembe v context


//user
            var user2 = new ApplicationUser  // dodamo novega userja not pa 
            {
                FirstName = "Tim",
                LastName = "Rus",
                Email = "tim@fri.si",
                NormalizedEmail = "XXXX@fri.si",
                UserName = "tim@fri.si",
                NormalizedUserName = "tim@fri.si",
                MobileNumber = "031123456",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user2.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user1,"Frijekul123$"); // geslo Hassed geslo 
                user2.PasswordHash = hashed;
                context.Users.Add(user2);
                
            }

            context.SaveChanges(); // shranim spremembe v context

// var tabelca z rolei to damo no pa smo vsi veseli pa debeli 
            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator"},
                new IdentityRole{Id="2", Name="Guest"},
            };

            foreach (IdentityRole r in roles)  // gremo cez z FOREAC stavkom pa dodajamo to sranje not pa smo veseli p
            {
                context.Roles.Add(r);
            } 


            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user1.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user2.Id},
            };

            foreach (IdentityUserRole<string> r in UserRoles)
            {
                context.UserRoles.Add(r);  // z zanko spet vse lepo dodam pol pa sam se lepo shranima pa je done deal
            }


            context.SaveChanges();  


        }
    }
}