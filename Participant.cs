using Discord;
using Discord.Rest;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class Participant
    {
        public string name;
        public ulong id;
        public ulong forager;
        public string action;
        public string weapon;
        public int target;

        // Constructors

        public Participant(string name, ulong id, ulong forager)
        {
            this.name = name;
            this.id = id;
            this.forager = forager;
        }

        public Participant(string name, ulong id, ulong forager, string action, string weapon, int target)
        {
            this.name = name;
            this.id = id;
            this.forager = forager;
            this.action = action;
            this.weapon = weapon;
            this.target = target;
        }
        
        public Participant()
        {

        }

        // Methods

        public string GetName()
        {
            return name;
        }

        public ulong GetID()
        {
            return id;
        }

        public string GetAction()
        {
            return action;
        }

        public string GetWeapon()
        {
            return weapon;
        }

        public int GetTarget()
        {
            return target;
        }

        public ulong GetForager()
        {
            return forager;
        }
    }
}
