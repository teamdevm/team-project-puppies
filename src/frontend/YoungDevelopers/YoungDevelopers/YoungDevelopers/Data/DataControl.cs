using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DogsCompanion.Api.Client;
using YoungDevelopers.Client;
using System.Net.Http;
using Newtonsoft.Json;

namespace YoungDevelopers
{
    public static class DataControl
    {
        public static TokenController tokenController = (TokenController)App.Current.Properties["tokenController"];
        public static DogsCompanionClient dogsCompanionClient = (DogsCompanionClient)App.Current.Properties["dogsCompanionClient"];
        public static HttpClient httpClient = (HttpClient)App.Current.Properties["httpClient"];
        public static string fileName = "data.json";
        public static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static string LoadData()
        {
            string DeserializeMessage = DeserializeFromFile();
            if (DeserializeMessage != "")
            {
                FillWithVoid();
                SerializeToFile();
                return DeserializeMessage;
            }
            return "";
        }

        public static string SerializeToFile()
        {
            if (App.Current.Properties["storedata"] != null)
            {
                try
                {
                    File.WriteAllText(Path.Combine(folderPath, fileName), JsonConvert.SerializeObject((Data)App.Current.Properties["storedata"]));

                    using (StreamWriter file = File.CreateText(Path.Combine(folderPath, fileName)))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, (Data)App.Current.Properties["storedata"]);
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return "";
        }

        public static string DeserializeFromFile()
        {
            if (File.Exists(Path.Combine(folderPath, fileName)))
            {
                try
                {
                    using (StreamReader file = File.OpenText(Path.Combine(folderPath, fileName)))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        App.Current.Properties["storedata"] = (Data)serializer.Deserialize(file, typeof(Data));
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return "Deserialization Fail";
        }

        public static void FillWithVoid()
        {
            App.Current.Properties["storedata"] = new Data();
            ((Data)App.Current.Properties["storedata"]).Users = new List<UserInfo>();
            ((Data)App.Current.Properties["storedata"]).Dogs = new List<ReadDog>();
            ((Data)App.Current.Properties["storedata"]).VetClinics = new List<VetClinic>();
            ((Data)App.Current.Properties["storedata"]).VetGroomerSalons = new List<GroomerSalon>();
        }

        public static void SetCurrentUser(AuthResponse authResponse)
        {
            App.Current.Properties["currentuserid"] = authResponse.Id;
        }

        public static void SetNewCurrentUser(RegisterResponse regResponse)
        {
            App.Current.Properties["currentuserid"] = regResponse.UserInfo.Id;
            ((Data)App.Current.Properties["storedata"]).Users.Add(regResponse.UserInfo);
            ((Data)App.Current.Properties["storedata"]).Dogs.Add(regResponse.DogInfo);
        }

        public static void SetAuthData(AuthResponse authResponse, ReadDog addDog)
        {
            try
            {
                int i = ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == authResponse.Id).Id;

                SetCurrentUser(authResponse);
            }
            catch (Exception e)
            {
                UserInfo addUser = new UserInfo();
                addUser.Id = authResponse.Id;
                addUser.Email = authResponse.Email;
                addUser.PhoneNumber = authResponse.PhoneNumber;
                addUser.FirstName = authResponse.FirstName;
                addUser.LastName = authResponse.LastName;
                addUser.MiddleName = authResponse.MiddleName;
                addUser.BirthDate = authResponse.BirthDate;

                ((Data)App.Current.Properties["storedata"]).Users.Add(addUser);
                SetCurrentUser(authResponse);
            }

            try
            {
                int j = ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.UserId == authResponse.Id).Id;
            }
            catch (Exception ex)
            {
                ((Data)App.Current.Properties["storedata"]).Dogs.Add(addDog);
            }

        }

        public static void LoadTestData()
        {
            App.Current.Properties["storedata"] = new Data();
            ((Data)App.Current.Properties["storedata"]).Users = PrefinedData.PredefinedUsers;
            ((Data)App.Current.Properties["storedata"]).Dogs = PrefinedData.PredefinedDogs;
            ((Data)App.Current.Properties["storedata"]).VetClinics = PrefinedData.PredefinedVetClinics;
            ((Data)App.Current.Properties["storedata"]).VetGroomerSalons = PrefinedData.PredefinedVetGroomerSalons;

            App.Current.Properties["currentuserid"] = 1;
        }

        public static UserInfo GetUserItem(int UserID)
        {
            return ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == UserID);
        }

        public static UserInfo GetCurrentUserItem()
        {
            int CurrentUserId = (int)App.Current.Properties["currentuserid"];
            return ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == CurrentUserId);
        }

        public static List<UserInfo> GetUserListItem(int UserID)
        {
            return ((Data)App.Current.Properties["storedata"]).Users;
        }

        public static ReadDog GetUserDogItem(int UserID)
        {
            return ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.UserId == UserID);
        }

        public static VetClinic GetVetClinicItem(int VetID)
        {
            return ((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == VetID);
        }

        public static List<VetClinic> GetVetClinicListItem()
        {
            return ((Data)App.Current.Properties["storedata"]).VetClinics;
        }

        public static GroomerSalon GetGroomingItem(int GroomID)
        {
            return ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == GroomID);
        }

        public static List<GroomerSalon> GetGroomerSalonListItem()
        {
            return ((Data)App.Current.Properties["storedata"]).VetGroomerSalons;
        }

        public static void SetGroomingItem(GroomerSalon groomerSalon)
        {
            if (((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == groomerSalon.Id) == null)
            {
                ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.Add(groomerSalon);
            }
            else
            {
                ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == groomerSalon.Id).Address = groomerSalon.Address;
                ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == groomerSalon.Id).Rating = groomerSalon.Rating;
                ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == groomerSalon.Id).Link = groomerSalon.Link;
                ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == groomerSalon.Id).Name = groomerSalon.Name;
                ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == groomerSalon.Id).PhoneNumber = groomerSalon.PhoneNumber;
                ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == groomerSalon.Id).OpeningHours = groomerSalon.OpeningHours;
            }
        }

        public static void SetVetclinicItem(VetClinic vetClinic)
        {
            if (((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == vetClinic.Id) == null)
            {
                ((Data)App.Current.Properties["storedata"]).VetClinics.Add(vetClinic);
            }
            else
            {
                ((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == vetClinic.Id).Address = vetClinic.Address;
                ((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == vetClinic.Id).Rating = vetClinic.Rating;
                ((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == vetClinic.Id).Link = vetClinic.Link;
                ((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == vetClinic.Id).Name = vetClinic.Name;
                ((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == vetClinic.Id).PhoneNumber = vetClinic.PhoneNumber;
                ((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == vetClinic.Id).OpeningHours = vetClinic.OpeningHours;
                ((Data)App.Current.Properties["storedata"]).VetClinics.First(s => s.Id == vetClinic.Id).IsAllDay = vetClinic.IsAllDay;
            }
        }

        public static void SetUserInfoItem(UserInfo updateUser)
        {
            ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == updateUser.Id).Email = updateUser.Email;
            ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == updateUser.Id).PhoneNumber = updateUser.PhoneNumber;
            ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == updateUser.Id).FirstName = updateUser.FirstName;
            ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == updateUser.Id).LastName = updateUser.LastName;
            ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == updateUser.Id).MiddleName = updateUser.MiddleName;
            ((Data)App.Current.Properties["storedata"]).Users.First(s => s.Id == updateUser.Id).BirthDate = updateUser.BirthDate;
        }

        public static void SetReadDogItem(ReadDog updateDoge)
        {
            ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.Id == updateDoge.Id).Name = updateDoge.Name;
            ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.Id == updateDoge.Id).BirthDate = updateDoge.BirthDate;
            ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.Id == updateDoge.Id).Breed = updateDoge.Breed;
            ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.Id == updateDoge.Id).Weight = updateDoge.Weight;
        }

        public static string GetHoursString(bool AllDay, string weekDay, ICollection<Period> dayPeriod)
        {
            if (AllDay) return weekDay + ": КРУГЛОСУТОЧНО";
            return weekDay + ": " + (dayPeriod.Count == 0 ? "ВЫХОДНОЙ" : dayPeriod.First().Open.UtcDateTime.Hour.ToString() + ":00" + '-' + dayPeriod.Last().Open.UtcDateTime.Hour.ToString() + ":00");
        }

        public static string GetEmoji(bool value)
        {
            if (value)
            {
                return new Emoji(0x2714).ToString();
            }
            else
            {
                return new Emoji(0x274C).ToString();
            }
        }
    }
}
