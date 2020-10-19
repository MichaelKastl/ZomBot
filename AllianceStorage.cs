using System;
using System.Collections.Generic;
using System.Text;

namespace Zombie_Apocalypse_Discord_Bot
{
    public class AllianceStorage
    {
        public string allianceName { get; set; }
        public List<Item> storage { get; set; }

        public AllianceStorage(string allianceName, List<Item> storage)
        {
            this.allianceName = allianceName;
            this.storage = storage;
        }

        public AllianceStorage()
        {

        }
    }
}
