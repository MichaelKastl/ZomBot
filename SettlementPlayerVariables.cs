using System;
using System.Collections.Generic;
using System.Text;

namespace Zombie_Apocalypse_Discord_Bot
{
    [Serializable]
    public class SettlementPlayerVariables
    {
        public DateTimeOffset rebuildBegan { get; set; }
        public bool isRebuilding { get; set; }
        public TimeSpan rebuildTimer { get; set; }
        public Settlement settlementRebuilding { get; set; }

        public SettlementPlayerVariables()
        {

        }
        public SettlementPlayerVariables(DateTimeOffset rebuildBegan, bool isRebuilding, TimeSpan rebuildTimer, Settlement settlementRebuilding)
        {
            this.rebuildBegan = rebuildBegan;
            this.isRebuilding = isRebuilding;
            this.rebuildTimer = rebuildTimer;
            this.settlementRebuilding = settlementRebuilding;
        }
    }
}
