using System;

namespace ZooKeeper_Blazor
{
    public class Raptor : Bird, IPredator
    {
        public Raptor(string name)
        {
            emoji = "🦅";
            species = "raptor";
            this.name = name;
            reactionTime = 1; // reaction time 1 (fast)
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a raptor. RAAAAAAAAA 'Murica RAAAAAAA ");
            TaskProcess();
        }

        public void TaskProcess() // Priority is to hunt over fly over walkabout
        {
            TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "cat");
            if (TaskCheck == false)
            {
                TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "mouse");
                if (TaskCheck == false)
                {
                    TaskCheck = Fly(this, location.x, location.y, 2);
                    if (TaskCheck == false)
                    {
                        Walkabout(location.x, location.y);
                    }
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