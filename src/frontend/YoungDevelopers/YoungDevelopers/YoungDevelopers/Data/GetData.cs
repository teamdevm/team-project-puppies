using DogsCompanion.Api.Client;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace YoungDevelopers
{
    public class Data
    {
        public Data()
        {

        }
        public List<UserInfo> Users = new List<UserInfo>();
        public List<ReadDog> Dogs = new List<ReadDog>();
        public List<VetClinic> VetClinics = new List<VetClinic>();
        public List<GroomerSalon> VetGroomerSalons = new List<GroomerSalon>();
    }
}
