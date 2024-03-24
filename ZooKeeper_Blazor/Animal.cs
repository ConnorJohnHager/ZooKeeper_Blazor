using System;

namespace ZooKeeper_Blazor
{
    public class Animal : Occupant
    {
        public string name;
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
        public bool TaskCheck;
        public bool TurnCheck;

        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }
    }
}
