using System;

namespace ZooKeeper_Blazor
{
	public class Chick : Bird, IPrey, IPredator 
	{
        public Chick(string name)
        {
            emoji = "🐥";
            species = "chick";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(6, 10);
        }

        public int totalTurns { get; private set; } = 0;//track chick's total turns 

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick. Beepbeep.");
            totalTurns++;
            TaskProcess();
        }

        // from Connor
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

            // Original code from Menglin, updated by Connor
            TaskCheck = CheckForMaturity(this, 3); // Check if chick is old enough to randomly grow into a raptor, rooster, or vulture
            if (TaskCheck == true)
            {
                Random random = new Random();
                int choice = random.Next(10);

                if (choice < 2)
                {
                    Game.Replace(location.x, location.y, new Raptor(this.name));
                }
                else if (choice < 7)
                {
                    Game.Replace(location.x, location.y, new Rooster(this.name));
                }
                else
                {
                    Game.Replace(location.x, location.y, new Vulture(this.name));
                }
            }

            // Original code by Menglin, updated by Connor
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

