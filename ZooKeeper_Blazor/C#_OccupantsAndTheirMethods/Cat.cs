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

        // from Connor
        public void TaskProcess() // Priority is to flee over hunt over walkabout
        {
            TaskCheck = (this as IPrey).Flee(this, location.x, location.y, "raptor", 2);
            if (TaskCheck == false)
            {
                TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "rooster");
                if (TaskCheck == false)
                {
                    TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "chick");
                    if (TaskCheck == false)
                    {
                        TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "mouse");
                        if (TaskCheck == false)
                        {
                            Walkabout(location.x, location.y);
                        }
                    }
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
                CheckForOffspring(this, location.x, location.y, new Cat("baby"));
                TurnCheck = true;
            }
        }
    }
}

