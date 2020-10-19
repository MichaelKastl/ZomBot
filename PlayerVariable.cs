using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]

    public class PlayerVariable
    {
        readonly string playerName;
        private DateTimeOffset lastForage;
        public bool isForaging;
        public bool hasVisitedShop;
        public bool isDefending;
        public int defenseFights;

        //Constructors

        public PlayerVariable()
        {

        }

        public PlayerVariable(string playerName)
        {
            this.playerName = playerName;
        }

        public PlayerVariable(string playerName, bool isDefending, int defenseFights)
        {
            this.playerName = playerName;
            this.isDefending = isDefending;
            this.defenseFights = defenseFights;
        }

        public PlayerVariable(string playerName, DateTimeOffset lastForage)
        {
            this.playerName = playerName;
            this.lastForage = lastForage;
        }

        public PlayerVariable(string playerName, bool isForaging)
        {
            this.playerName = playerName;
            this.isForaging = isForaging;
        }

        public PlayerVariable(string playerName, DateTimeOffset lastForage, bool isForaging)
        {
            this.playerName = playerName;
            this.lastForage = lastForage;
            this.isForaging = isForaging;
        }

        //Methods

        public string GetPlayerName()
        {
            return playerName;
        }

        public DateTimeOffset GetLastForage()
        {
            return lastForage;
        }

        public bool GetIsForaging()
        {
            lock (this)
            {
                return isForaging;
            }
        }
        
        public bool GetHasVisitedShop()
        {
            return hasVisitedShop;
        }

        public bool StartForaging()
        {
            lock(this)
            {
                if (isForaging)
                {
                    return false;
                }
                else
                {
                    isForaging = true;
                    lastForage = DateTimeOffset.Now;
                    return true;
                }
            }
        }
    }
}
