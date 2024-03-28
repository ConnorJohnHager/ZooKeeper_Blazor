﻿using System;
namespace ZooKeeper_Blazor
{
	public class Vulture: Bird, IPredator
	{
		public Vulture(string name)
		{
            emoji = "🐦‍⬛";
            species = "vulture";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(2, 4);
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a vulture. Fear me!");
            TaskProcess();
        }

        public void TaskProcess() // Priority is to hunt over fly over walkabout
        {
            TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "corpse");
            if (TaskCheck == false)
            {
                TaskCheck = Fly(this, location.x, location.y, 2);
                if (TaskCheck == false)
                {
                    Walkabout(location.x, location.y);
                }
            }

            TaskCheck = CheckForDeath(this); // Check if the animal has eaten within the required number of turns or has died
            if (TaskCheck == true)
            {
                Game.Replace(location.x, location.y, new Corpse());
            }
            else
            {
                age++;
                TurnCheck = true;
            }
        }
    }
}
