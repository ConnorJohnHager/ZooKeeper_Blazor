using System;

namespace ZooKeeper_Blazor
{
	public class Chick : Bird, IPrey
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
            TaskProcess();
        }

        public void TaskProcess()
        {
            TaskCheck = (this as IPrey).Flee(this, location.x, location.y, "cat");
            TurnCheck = true;
        }
    }
}

