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
            new User {Id = 1, Email = "spistsov@gmail.com", Password = "", PhoneNumber = "8800553535", FirstName = "Сергей", LastName = "Писцов", MiddleName = "Михайлович", BirthDate = new DateTime(2001, 10, 20)},
            new User {Id = 2, Email = "patpevhit@el.ar", Password = "", PhoneNumber = "(336) 796-8869", FirstName = "Olive", LastName = "Briggs", MiddleName = "Jimmy", BirthDate = new DateTime(2000, 1, 5)},
            new User {Id = 3, Email = "gegsigzo@vejoku.je", Password = "", PhoneNumber = "(936) 997-2677", FirstName = "Nora", LastName = "Nguyen", MiddleName = "Alfred", BirthDate = new DateTime(1997, 5, 16)},
            new User {Id = 4, Email = "escauki@vocza.ls", Password = "", PhoneNumber = "(886) 224-7267", FirstName = "Hettie", LastName = "Thomas", MiddleName = "Virginia", BirthDate = new DateTime(2001, 4, 12)},
        };

        public static List<Dog> PredefinedDogs => new List<Dog>
        {
            new Dog { Id = 1, Name = "Бен", Breed = "Бернедудл", Weight = 30, BirthDate = new DateTime(1993, 1, 1) , UserId = 1},
            new Dog { Id = 2, Name = "Барбарик", Breed = "Английский бульдог", Weight = 12, BirthDate = new DateTime(2017, 12, 5), UserId = 2},
            new Dog { Id = 3, Name = "Арчи", Breed = "Овчарка", Weight = 20, BirthDate = new DateTime(2015, 7, 7) , UserId = 3},
        };

        public static List<VetClinic> PredefinedVetClinics => new List<VetClinic>
        {
            new VetClinic { Id = 1, Name = "Друг", Address = "Ulitsa Geroyev Khasana, 7а", Link = "https://vetdrugperm.ru/", PhoneNumber = "8 (342) 244-11-51",
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

            new VetClinic { Id = 2, Name = "Клык+", Address = "Bul'var Gagarina, 70Б", Link = "https://bkperm.ru/", PhoneNumber = "8 (342) 270-02-18",
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

        };

        public static List<GroomerSalon> PredefinedVetGroomerSalons => new List<GroomerSalon>
        {
            new GroomerSalon { Id = 1, Address = "Пушкина 5а", Name = "Dog is Dog", Link = "https://dogisdog.ru/", PhoneNumber = "8 (342) 279-83-39", OpeningHours = ""},
        };
    }
}
