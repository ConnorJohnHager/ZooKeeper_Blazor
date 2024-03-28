using System;
namespace ZooKeeper_Blazor
{
    // from Menglin
    public class Corpse : Occupant
	{
        public int turnsRemaining = 3;

        public Corpse()
		{
            emoji = "💀";
            species = "corpse";
        }
	}
}

