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

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick. Beepbeep.");
            turnsSinceLastHunt++;
            TaskProcess();
        }

        public void TaskProcess()
        {
            TaskCheck = (this as IPrey).Flee(this, location.x, location.y, "cat", 1);
            if (TaskCheck == false)
            {
                TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "grass");
            }
            TurnCheck = true;
        }
    }
}

