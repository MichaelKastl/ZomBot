using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class Distractor
    {
        public string name;
        public ulong id;
        public int target;
        public int charisma;

        public Distractor(string name, int target, int charisma, ulong id)
        {
            this.name = name;
            this.target = target;
            this.charisma = charisma;
            this.id = id;
        }
    }
}
