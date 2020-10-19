using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class ServerData
    {
        string prefix;
        string name;
        ulong owner;
        public ulong serverId;
        public ulong hordeChannel;
        public DateTimeOffset lastHordeAttack;
        public DateTimeOffset lastHordeCheck;
        public TimeSpan dustyRestockInterval;
        public DateTimeOffset lastBackup;

        //Constructors

        public ServerData()
        {

        }

        public ServerData(string prefix, string name, ulong owner, ulong hordeChannel)
        {
            this.prefix = prefix;
            this.name = name;
            this.owner = owner;
            this.hordeChannel = hordeChannel;
            lastHordeAttack = DateTimeOffset.MinValue;
            lastHordeCheck = DateTimeOffset.MinValue;
            lastBackup = DateTimeOffset.MinValue;
        }

        // Methods

        public string GetPrefix()
        {
            return prefix;
        }

        public string GetName()
        {
            return name;
        }

        public ulong GetOwner()
        {
            return owner;
        }
    }
}
