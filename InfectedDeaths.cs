using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class InfectedDeaths
    {
        List<string> users;

        //Constructors

        public InfectedDeaths()
        {

        }

        public InfectedDeaths(List<string> users)
        {
            this.users = users;
        }

        public InfectedDeaths(string user)
        {
            users.Add(user);
        }

        //Methods

        public List<String> GetUsers()
        {
            return users;
        }
    }
}
