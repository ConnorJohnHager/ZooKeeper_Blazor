using System;

namespace ZooKeeper_Blazor
{
    public class Cat : Animal, IPredator, IPrey
    {
        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)Cat
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a cat. Meow.");
            TaskProcess();
        }

        public void TaskProcess() // Priority is to flee over hunt
        {
            TaskCheck = (this as IPrey).Flee(this, location.x, location.y, "raptor");
            if (TaskCheck == false)
            {
                TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "mouse");
                if (TaskCheck == false)
                {
                    TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "chick");
                }
            }
            TurnCheck = true;
        }
    }
}

