using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class Charsheet
    {
        public string playerName;

        // Player Resources
        public int hunger;
        public int thirst;
        public int health;
        public int infection;
        public int influence;
        public int cash;

        // Player Stats
        public int strength;
        public int intelligence;
        public int charisma;
        public int constitution;
        public int level;
        public int skillPoints;
        public int exp;
        public int scrap;
        public string alliance;
        public int biteSeverity;

        //Constructors

        public Charsheet (string user, int health, int hunger, int thirst, int infection, int influence, int cash, int strength, int intelligence, int charisma, int constitution, int level, int skillPoints, int exp, int scrap, string alliance, int biteSeverity)
        {
            this.playerName = user;
            this.health = health;
            this.hunger = hunger;
            this.thirst = thirst;
            this.infection = infection;
            this.influence = influence;
            this.cash = cash;
            this.strength = strength;
            this.intelligence = intelligence;
            this.charisma = charisma;
            this.constitution = constitution;
            this.level = level;
            this.skillPoints = skillPoints;
            this.exp = exp;
            this.scrap = scrap;
            this.alliance = alliance;
            this.biteSeverity = biteSeverity;
        }

        public Charsheet (string user)
        {
            playerName = user;
            hunger = 100;
            thirst = 100;
            health = 100;
            infection = 0;
            influence = 0;
            cash = 0;
            strength = 0;
            intelligence = 0;
            charisma = 0;
            constitution = 0;
            level = 1;
            skillPoints = 0;
            exp = 0;
            scrap = 0;
            alliance = "None";
            biteSeverity = 0;
        }

        public Charsheet ()
        {

        }

        //Methods 

        public string GetName()
        {
            return playerName;
        }

        public int GetHunger()
        {
            return hunger;
        }

        public int GetThirst()
        {
            return thirst;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetInfection()
        {
            return infection;
        }

        public int GetInfluence()
        {
            return influence;
        }

        public int GetCash()
        {
            return cash;
        }

        public int GetStrength()
        {
            return strength;
        }

        public int GetIntelligence()
        {
            return intelligence;
        }

        public int GetCharisma()
        {
            return charisma;
        }

        public int GetConstitution()
        {
            return constitution;
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetSkillPoints()
        {
            return skillPoints;
        }

        public int GetExp()
        {
            return exp;
        }

        public int GetScrap()
        {
            return scrap;
        }

        public string GetAlliance()
        {
            return alliance;
        }

        public int GetBiteSeverity()
        {
            return biteSeverity;
        }
    }
}
