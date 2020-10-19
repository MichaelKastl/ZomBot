using System;
using System.Collections.Generic;
using System.Text;

namespace Zombie_Apocalypse_Discord_Bot
{
    public class Invitee
    {
        readonly ulong inviterID;
        readonly ulong id;
        readonly string name;

        //Constructors

        public Invitee()
        {

        }

        public Invitee(ulong inviterID, ulong id, string name)
        {
            this.inviterID = inviterID;
            this.id = id;
            this.name = name;
        }

        //Methods

        public ulong GetInviterID()
        {
            return inviterID;
        }

        public ulong GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }
    }
}
