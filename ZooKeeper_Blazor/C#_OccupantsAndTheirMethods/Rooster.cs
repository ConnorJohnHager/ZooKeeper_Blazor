using System;
namespace ZooKeeper_Blazor
{
	public class Rooster : Bird, IPredator, IPrey
    {
		public Rooster(string name)
        {
            emoji = "🐓";
            species = "rooster";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(3, 6);
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a rooster. Bacaw!");
            TaskProcess();
        }

        public void TaskProcess() // Priority is to flee over hunt over walkabout
        {
            TaskCheck = (this as IPrey).Flee(this, location.x, location.y, "cat", 1);
            if (TaskCheck == false)
            {
                TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "grass");
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

