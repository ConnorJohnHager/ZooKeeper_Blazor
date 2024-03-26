using System;
namespace ZooKeeper_Blazor
{
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

