using System;

namespace ZooKeeper_Blazor
{
    public class Animal : Occupant
    {
        public string name;
        public int age = 0; // all new animals default starting at 0
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
        public bool TaskCheck;
        public bool TurnCheck;
        
        public int turnsForHunting = 6; // default turns given for hunting food (five turns but adjusted for processing)
        public int turnsSinceLastHunt { get; set; } = 1; // tracks the non-eating turns, starting with 1 for processing

        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }

        // from Connor, based on code from Valentina
        public bool Walkabout(int x, int y) // check all directions for animal to wander around the board
        {
            Random random = new Random();
            List<Direction> possibleDirections = new List<Direction> { Direction.up, Direction.down, Direction.left, Direction.right};

            if (!Game.Seek(x, y, Direction.up, "null", 1))
            {
                possibleDirections.Remove(Direction.up);
            }
            if (!Game.Seek(x, y, Direction.down, "null", 1))
            {
                possibleDirections.Remove(Direction.down);
            }
            if (!Game.Seek(x, y, Direction.left, "null", 1))
            {
                possibleDirections.Remove(Direction.right);
            }
            if (!Game.Seek(x, y, Direction.right, "null", 1))
            {
                possibleDirections.Remove(Direction.left);
            }

            if (possibleDirections.Count > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];

                if (Game.Move(this, moveDirection, 1) > 0) return true;
            }
            return false;
        }

        // Original code by Menglin, updated by Connor
        public bool CheckForMaturity(Animal animal, int adulthood) // check if an animal is old enough to grow up or reproduce
        {
            if (animal.age == adulthood)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Original code by Menglin, updated by Connor
        public bool CheckForDeath(Animal animal) // check if animal hasn't eaten within the required amount of turns
        {
            if (animal.turnsSinceLastHunt == animal.turnsForHunting)
            {
                return true;
            }
            else
            {
                animal.turnsSinceLastHunt++;
            }
            return false;
        }
    }
}
