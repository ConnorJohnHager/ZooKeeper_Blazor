using System;

namespace ZooKeeper_Blazor
{
    public class Mouse : Animal, IPrey
    {
        public Mouse(string name)
        {
            emoji = "🐭";
            species = "mouse";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(1, 4); // reaction time of 1 (fast) to 3
            /* Note that Mouse reactionTime range is smaller than Cat reactionTime,
             * so mice are more likely to react to their surroundings faster than cats!
             Mouse*/
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a mouse. Squeak.");
            TaskProcess();
        }

        public void TaskProcess()
        {
            TaskCheck = (this as IPrey).Flee(this, location.x, location.y, "raptor");
            if (TaskCheck == false)
            {
                TaskCheck = (this as IPrey).Flee(this, location.x, location.y, "cat");
            }
            TurnCheck = true;
        }
    }
}