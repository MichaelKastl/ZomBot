using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]
    public class Entry
    {
        public object Key;
        public object Value;
        public Entry()
        {
        }

        public Entry(object key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
