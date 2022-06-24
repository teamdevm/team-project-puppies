using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using DogsCompanion.Api.Client;
using YoungDevelopers.Client;
using System.Net.Http;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace YoungDevelopers
{
    public static class DataControl
    {
        // Get Client
        public static TokenController tokenController = (TokenController)App.Current.Properties["tokenController"];
        public static DogsCompanionClient dogsCompanionClient = (DogsCompanionClient)App.Current.Properties["dogsCompanionClient"];
        public static HttpClient httpClient = (HttpClient)App.Current.Properties["httpClient"];

        public static string fileName = "data.json";
        public static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static string LoadData()
        {
            return DeserializeFromFile();
            //LoadTestData();
        }
        public static void ClearAllData()
        {
            App.Current.Properties["storedata"] = null;
            App.Current.Properties["storedata"] = new Data();
        }

        public static void FillWithVoid()
        {
            App.Current.Properties["storedata"] = null;
            Data data = new Data();
            data.Dogs.Add
            (

                new ReadDog { Id = 1, Name = "", Breed = "", Weight = 0, BirthDate = new DateTime(2000, 1, 1), UserId = 1 }
            );
            data.Users.Add
            (
                new UserInfo { Id = 1, Email = "", PhoneNumber = "", FirstName = "", LastName = "", MiddleName = "", BirthDate = new DateTime(2000, 1, 1) }
            );
            App.Current.Properties["storedata"] = data;
        }


        public static void LoadFromServer()
        {

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
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return "";
        }

        public static void UpdateProperties()
        {

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
        

        public static void LoadTestData()
        {
            App.Current.Properties["storedata"] = new Data();
            ((Data)App.Current.Properties["storedata"]).Users = PrefinedData.PredefinedUsers;
            ((Data)App.Current.Properties["storedata"]).Dogs = PrefinedData.PredefinedDogs;
            ((Data)App.Current.Properties["storedata"]).VetClinics = PrefinedData.PredefinedVetClinics;
            ((Data)App.Current.Properties["storedata"]).VetGroomerSalons = PrefinedData.PredefinedVetGroomerSalons;

            App.Current.Properties["currentuserid"] = 1;
        }

        // No ID need?
        //public static string GetStringItem(DataControl.Item item)
        //{
        //    int UserID = (int)App.Current.Properties["currentuserid"];
        //    if (item == DataControl.Item.DogName)
        //    {
        //        return ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.UserId == UserID).Name;
        //    }
        //    else if (item == DataControl.Item.DogBreed)
        //    {
        //        return ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.UserId == UserID).Breed;
        //    }
        //    else if (item == DataControl.Item.UserEmail)
        //    {

        //    }
        //    // АААА
        //    else if (item == DataControl.Item.UserPassword)
        //    {

        //    }
        //    else if (item == DataControl.Item.UserPhoneNumber)
        //    {

        //    }
        //    else if (item == DataControl.Item.UserFirstName)
        //    {

        //    }
        //    else if (item == DataControl.Item.UserLastName)
        //    {

        //    }
        //    else if (item == DataControl.Item.UserMiddleName)
        //    {

        //    }

        //}

        //public static string GetStringItemID(int ID, DataControl.Item item)
        //{
        //    if (item == DataControl.Item.GroomerSalonName)
        //    {
        //        return ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == ID).Name;
        //    }
        //    else if (item == DataControl.Item.GroomerSalonAddress)
        //    {
        //        return ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == ID).Address;
        //    }
        //    else if (item == DataControl.Item.GroomerSalonPhoneNumber)
        //    {
        //        return ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == ID).PhoneNumber;
        //    }
        //    else if (item == DataControl.Item.GroomerSalonLink)
        //    {
        //        return ((Data)App.Current.Properties["storedata"]).VetGroomerSalons.First(s => s.Id == ID).Link;
        //    }
        //    else if (item == DataControl.Item.VetClinicName)
        //    {

        //    }
        //    else if (item == DataControl.Item.VetClinicAddress)
        //    {

        //    }
        //    else if (item == DataControl.Item.VetClinicPhoneNumber)
        //    {


        //    }
        //    else if (item == DataControl.Item.VetClinicLink)
        //    {


        //    }
        //}


        //public static bool GetBooleanItem(DataControl.Item item)
        //{

        //    VetClinicIsAllDay;
        //}

        //public static double GetDoubleItem(DataControl.Item item)
        //{

        //    GroomerSalonRating;
        //    VetClinicRating;
        //}

        //public static int GetIntItem(DataControl.Item item)
        //{
        //    DogWeight;
        //}

        //public static DateTime GetDateItem(DataControl.Item item)
        //{
        //    return DateTime.Now;
        //    UserBirthDate;
        //    DogBirthDate;

        //}

        //public static User GetUserItem(DataControl.Item item)
        //{
        //    DogUser;
        //    return null;
        //}

        //public static Dog GetDogItem(DataControl.Item item)
        //{
        //    UserDog;
        //    return null;
        //}

        //public static OpeningHours GetOpeningHoursItem(DataControl.Item item)
        //{
        //    GroomerSalonOpeningHours;
        //    VetClinicOpeningHours;
        //    return null;
        //}

        // Получение данных из Data
        public static UserInfo GetUserItem(int UserID)
        {
            //return ((Data)App.Current.Properties["storedata"]).Dogs.First(s => s.UserId == UserID).Name;
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

        public static UserInfo UpdateUser()
        {
            //int UserID = (int)App.Current.Properties["currentuserid"];
            return null;
        }

        public async static void LoadTestUser()
        {
            string email = "spistsov@gmail.com";
            string password = "Billy1!";
            SendAuth sendauth = new SendAuth();
            sendauth.email = email;
            sendauth.password = password;

            var dogsCompanionClient = DataControl.dogsCompanionClient;
            var httpClient = DataControl.httpClient;
            var tokenController = DataControl.tokenController;


            try
            {
                var authInfo = new AuthInfo()
                {
                    Email = email,
                    Password = password,
                };

                var authResponse = await dogsCompanionClient.AuthenticateAsync(authInfo);

                // Установление ключа в httpclient
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResponse.AccessToken);

                // Сохранение токенов в хранилище
                await tokenController.SetRefreshTokenAsync(authResponse.RefreshToken);
                await tokenController.SetAccessTokenAsync(authResponse.AccessToken);

                // Установить текущего пользователя
                DataControl.SetCurrentUser(authResponse);

                // Переход на главную страницу
                // TODO сохранить полученные данные из authInfo
                // TODO поменять Main страницу
            }
            catch (ApiException apiExc)
            {
                if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)
                {
                }
                else if (apiExc.StatusCode == StatusCodes.Status401Unauthorized)
                {
                }
            }
            catch (Exception)
            {
            }
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
        public enum Item
        {
            User,
            Dog,
            VetClinic,
            Grooming,
            UserEmail,
            UserPassword,
            UserPhoneNumber,
            UserFirstName,
            UserLastName,
            UserMiddleName,
            UserBirthDate, //
            UserDog,
            DogName,
            DogBirthDate, //
            DogBreed,
            DogWeight,
            DogUserID,
            DogUser, //
            GroomerSalonName,
            GroomerSalonAddress,
            GroomerSalonPhoneNumber,
            GroomerSalonLink,
            GroomerSalonRating,
            GroomerSalonOpeningHours, //
            VetClinicName,
            VetClinicAddress,
            VetClinicPhoneNumber,
            VetClinicLink,
            VetClinicRating,
            VetClinicIsAllDay,
            VetClinicOpeningHours, //
        };


    }
}
