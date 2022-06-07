using DogsCompanion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.Data
{
    public static class DogsCompanionContextSeed
    {
        public static List<User> PredefinedUsers => new List<User>
        {
            new User {Id = 1, Email = "spistsov@gmail.com", Password = "$2a$11$XOzdFbzH.gzBCr0jfxK8UuCsrH2WI3zfG0EDv8HbRv9UVeuvKIcfm", PhoneNumber = "8800553535", FirstName = "Сергей", LastName = "Писцов", MiddleName = "Михайлович", BirthDate = new DateTime(2001, 10, 20)}, // Billy1!
            new User {Id = 2, Email = "adonis7952@gmail.com", Password = "$2a$11$dgP3ahlPEZQm9NZNFCmkduf5Gw8p.D86la2pGLO7.w78LjiLjLs4u", PhoneNumber = "89523335325", FirstName = "Василий", LastName = "Балчиков", MiddleName = "Игоревич", BirthDate = new DateTime(2001, 4, 13)}, // Admin123@
            new User {Id = 3, Email = "katya.myr.23@yandex.ru", Password = "$2a$11$BYe4VldPpK9mRnNNUfwsouH9xotylruy9REIVyXcaNna/vrZeULyi", PhoneNumber = "89525915325", FirstName = "Екатерина", LastName = "Гладких", MiddleName = "Алексеевна", BirthDate = new DateTime(2001, 10, 19)}, // simplePassword1!
            new User {Id = 4, Email = "ryan.gosling@gmail.com", Password = "$2a$11$fO1sx5ja9Ye8GWJpcJmwleGTkVuKhT.TTIjb4URqIYDdidlgKv.7y", PhoneNumber = "8869553535", FirstName = "Ryan", LastName = "Gosling", MiddleName = "Thomas", BirthDate = new DateTime(1980, 11, 12)}, // DOGSdogs!123
        };

        public static List<Dog> PredefinedDogs => new List<Dog>
        {
            new Dog { Id = 1, Name = "Бен", Breed = "Бернедудл", Weight = 30, BirthDate = new DateTime(1993, 1, 1) , UserId = 1},
            new Dog { Id = 2, Name = "Барбарик", Breed = "Английский бульдог", Weight = 12, BirthDate = new DateTime(2017, 12, 5), UserId = 2},
            new Dog { Id = 3, Name = "Арчи", Breed = "Овчарка", Weight = 20, BirthDate = new DateTime(2015, 7, 7) , UserId = 3},
        };

        public static List<VetClinic> PredefinedVetClinics => new List<VetClinic>
        {
            new VetClinic { Id = 1, Name = "Друг", Address = "Ulitsa Geroyev Khasana, 7а", Link = "https://vetdrugperm.ru/", PhoneNumber = "8 (342) 244-11-51", IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Days.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(12), Close = new DateTime().AddHours(18) } }
                    }
                }
            },

            new VetClinic { Id = 2, Name = "Клык+", Address = "Bul'var Gagarina, 70Б", Link = "https://bkperm.ru/", PhoneNumber = "8 (342) 270-02-18", IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Days.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(12), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Days.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(12), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Days.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(12), Close = new DateTime().AddHours(18) } }
                    }
                }
            },

            new VetClinic { Id = 3, Name = "Emancipet", Address = "Ulitsa Ursha, 75", Link = "https://emancipet.org/", PhoneNumber = "8 (800) 555-35-35", IsAllDay = true
            },
        };

        public static List<GroomerSalon> PredefinedVetGroomerSalons => new List<GroomerSalon>
        {
            new GroomerSalon { Id = 1, Address = "Пушкина 5а", Name = "Dog is Dog", Link = "https://dogisdog.ru/", PhoneNumber = "8 (342) 279-83-39"},
        };
    }
}
