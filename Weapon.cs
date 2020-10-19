using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [XmlInclude(typeof(Weapon))]
    [Serializable]
    public class Weapon
    {
        public string name;
        public string category;
        public string damageType;
        public int damage;
        public string tag1;
        public string tag2;
        public string tag3;
        public string tag4;
        public string tag5;

        public Weapon(string name, string category, string damageType, int damage, string tag1, string tag2, string tag3, string tag4, string tag5)
        {
            this.name = name;
            this.category = category;
            this.damageType = damageType;
            this.damage = damage;
            this.tag1 = tag1;
            this.tag2 = tag2;
            this.tag3 = tag3;
            this.tag4 = tag4;
            this.tag5 = tag5;
        }

        public Weapon()
        {

        }
    }
}
