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
        };

        public static List<Dog> PredefinedDogs => new List<Dog>
        {
            new Dog { Id = 1, Name = "Бен", Breed = "Бернедудл", Weight = 30, BirthDate = new DateTime(1993, 1, 1) , UserId = 1},
            new Dog { Id = 2, Name = "Барбарик", Breed = "Английский бульдог", Weight = 12, BirthDate = new DateTime(2017, 12, 5), UserId = 2},
            new Dog { Id = 3, Name = "Арчи", Breed = "Овчарка", Weight = 20, BirthDate = new DateTime(2015, 7, 7) , UserId = 3},
        };

        public static List<VetClinic> PredefinedVetClinics => new List<VetClinic>
        {
             new VetClinic { Id = 1, Name = "Dogmas", Address = "ул. Куйбышева, 1", Link = @"https://vk.com/public211658918", PhoneNumber = "+7 (342) 203-03-43", Rating = 0.0, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    }
                }
            },
            new VetClinic { Id = 2, Name = "Deutsche Vet", Address = "ул. Екатерининская ул., 51", Link = @"дойчевет.рф", PhoneNumber = "+7 (342) 212-68-37", Rating = 4.4, IsAllDay = true,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    }
                }
            },
            new VetClinic { Id = 3, Name = "АльфаВет", Address = "ул. Пушкина, 23", Link = @"alfavet59.ru", PhoneNumber = "+7 (342) 247-43-44", Rating = 4.4, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(19) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(19) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(19) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(19) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(19) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(17) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(17) } }
                    }
                }
            },
            new VetClinic { Id = 4, Name = "Друг", Address = "ул. Героев Хасана, 7А", Link = @"vetdrugperm.ru", PhoneNumber = "+7 (342) 244-11-51", Rating = 4.7, IsAllDay = true,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    }
                }
            },
            new VetClinic { Id = 5, Name = "Неболит", Address = "ул. Механошина, 15", Link = null, PhoneNumber = "+7 (342) 293-97-92", Rating = 4.2, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    }
                }
            },
            new VetClinic { Id = 6, Name = "Ветеринарная клиника Ника", Address = "ул. Мильчакова, 3, этаж 1", Link = @"nikavetperm.ru", PhoneNumber = "+7 (922) 344-01-01", Rating = 4.3, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new VetClinic { Id = 7, Name = "Animals Health", Address = "ул. Чернышевского, 23", Link = @"https://vk.com/animals_health_perm", PhoneNumber = "+7 (342) 203-72-73", Rating = 4.4, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new VetClinic { Id = 8, Name = "Друг", Address = "ул. Тургенева, 12", Link = @"vetdrugperm.ru", PhoneNumber = "+7 (342) 265-51-99", Rating = 4.7, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    }
                }
            },

            new VetClinic { Id = 9, Name = "КТвет", Address = "ул. Чернышевского, 28", Link = @"ktvet.ru", PhoneNumber = "+7 (342) 248-42-17", Rating = 4.5, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(18) } }
                    }
                }
            },
            new VetClinic { Id = 10, Name = "Доктор Вет", Address = "ул. Лебедева, 25Б", Link = @"doktorvet-perm.wixsite.com", PhoneNumber = "+7 (342) 247-01-09", Rating = 4.5, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new VetClinic { Id = 11, Name = "Ветеринарная клиника ВетОптТорг", Address = "ул. Лифанова, 2, этаж 1", Link = @"vetopttorg.ru", PhoneNumber = "+7 (919) 480-29-79", Rating = 4.3, IsAllDay = true,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(10) } }
                    }
                }
            },
            new VetClinic { Id = 12, Name = "Профессорская ветеринарная клиника", Address = "ул. Работницы, 2", Link = @"https://vk.com/club26456139", PhoneNumber = "+7 (342) 243-33-96", Rating = 4.3, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new VetClinic { Id = 13, Name = "Ветклиника Неовет", Address = "ул. Юрша, 25", Link = @"neovet24.ru", PhoneNumber = "+7 (342) 299-49-09", Rating = 4.1, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20).AddMinutes(30) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20).AddMinutes(30) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20).AddMinutes(30) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20).AddMinutes(30) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20).AddMinutes(30) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(19) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(19) } }
                    }
                }
            },
            new VetClinic { Id = 14, Name = "Vetskin", Address = "ул. Макаренко, 54", Link = @"vetskin.ru", PhoneNumber = "+7 (342) 286-22-20", Rating = 4.0, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(17) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        {  } // Выходной
                    }
                }
            },
            new VetClinic { Id = 15, Name = "Профессорская ветеринарная клиника", Address = "ул. Вильямса, 14", Link = @"https://vk.com/club26456139", PhoneNumber = "+7 (342) 202-23-66", Rating = 4.4, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                                        new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new VetClinic { Id = 16, Name = "Гайвинская ветеринарная клиника", Address = "ул. Вильямса, 4А,этаж 0", Link = @"https://vk.com/club105784741", PhoneNumber = "+7 (908) 271-48-39", Rating = 4.3, IsAllDay = false,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(17) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(17) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(17) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(17) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(17) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(15) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        {  }
                    }
                }
            },
        };

        public static List<GroomerSalon> PredefinedVetGroomerSalons => new List<GroomerSalon>
        {
            new GroomerSalon { Id = 1, Name = "Dog is Dog", Address = "ул. Луначарского, 90, этаж 1", Link = @"dogisdog.ru", PhoneNumber = "+7 (342) 202-00-93", Rating = 5.0,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    }
                }
            },
            new GroomerSalon { Id = 2, Name = "Стрижка собак и кошек GROOMi", Address = "ул. Глеба Успенского, 22", Link = @"groomi.bitrix24.site", PhoneNumber = "+7 (992) 223-33-16", Rating = 4.2,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new GroomerSalon { Id = 3, Name = "Модный енот", Address = "ул. Мира, 3, этаж 1", Link = @"www.salonenot.ru", PhoneNumber = "+7 (342) 277-11-07", Rating = 4.4,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    }
                }
            },
            new GroomerSalon { Id = 4, Name = "Mr. Sобакен", Address = "ул. Куйбышева, 70", Link = @"https://vk.com/mrsobakenperm", PhoneNumber = "+7 (958) 872-12-27", Rating = 5.0,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        {  }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    }
                }
            },
            new GroomerSalon { Id = 5, Name = "Pet SPA", Address = "ул. Пушкина, 66", Link = null, PhoneNumber = "+7 (342) 202-20-70", Rating = 4.4,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new GroomerSalon { Id = 6, Name = "Dog is Dog", Address = "ул. Николая Островского, 93Д", Link = @"www.dogisdog.ru", PhoneNumber = "+7 (342) 279-83-39", Rating = 4.9,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    }
                }
            },
            new GroomerSalon { Id = 7, Name = "Цаца", Address = "1-я Красноармейская ул., 41", Link = @"https://vk.com/id417548559", PhoneNumber = "+7 (342) 203-04-06", Rating = 4.2,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new GroomerSalon { Id = 8, Name = "Groomi", Address = "ш. Космонавтов, 121", Link = @"groomi.bitrix24.site", PhoneNumber = "+7 (992) 223-33-16", Rating = 0.0,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(21) } }
                    }
                }
            },
            new GroomerSalon { Id = 9, Name = "Артемон и К", Address = "ул. Вильямса, 4А", Link = null, PhoneNumber = "+7 (908) 241-65-73", Rating = 4.1,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(11), Close = new DateTime().AddHours(20) } }
                    }
                }
            },
            new GroomerSalon { Id = 10, Name = "Дама с собачкой", Address = "Кронштадтская ул., 43, этаж 1, офис 2", Link = @"https://vk.com/club152463988", PhoneNumber = "+7 (912) 589-81-68", Rating = 4.3,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(18) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(10), Close = new DateTime().AddHours(18) } }
                    }
                }
            },
            new GroomerSalon { Id = 11, Name = "Love is dogs", Address = "ул. Мира, 122/1", Link = @"https://vk.com/loveisdogs_perm", PhoneNumber = "+7 (908) 259-26-26", Rating = 4.9,
                OpeningHours = new List<OpeningHours>
                {
                    new OpeningHours
                    {
                        Day = Day.Mon,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Tue,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Wed,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Thu,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Fri,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sat,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20) } }
                    },
                    new OpeningHours
                    {
                        Day = Day.Sun,
                        Periods = new List<Period>
                        { new Period {Open = new DateTime().AddHours(9), Close = new DateTime().AddHours(20) } }
                    }
                }
            },
        };
    }
}
