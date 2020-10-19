using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class StandardZombie
    {
        public int health;
        public int armor;
        public int damage;
        public string ID;
        public string forager;

        public StandardZombie(int health, int armor, int damage, string ID, string forager)
        {
            this.health = health;
            this.armor = armor;
            this.damage = damage;
            this.ID = ID;
            this.forager = forager;
        }

        public StandardZombie(string ID)
        {
            this.ID = ID;
            health = 10;
            armor = 5;
            damage = 5;
        }

        public StandardZombie()
        {

        }
    }
}
