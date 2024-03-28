using System;

namespace ZooKeeper_Blazor
{
    public class Mouse : Animal, IPrey, IPredator
    {
        public Mouse(string name)
        {
            emoji = "🐭";
            species = "mouse";
            this.name = name;
            reactionTime = new Random().Next(1, 4);
 
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a mouse. Squeak.");
            TaskProcess();
        }

        // from Connor
        public void TaskProcess() // Priority is to flee over hunt over walkabout
        {
            TaskCheck = TotalFlee(location.x, location.y, "raptor");
            if (TaskCheck == false)
            {
                TaskCheck = TotalFlee(location.x, location.y, "cat");
                if (TaskCheck == false)
                {
                    TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "grass");
                    if (TaskCheck == false)
                    {
                        Walkabout(location.x, location.y);
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
                TurnCheck = true;
            }
        }



        // from Valentina
        public Direction Flee(int x, int y, string predator)
        {
            Random random = new Random();
            List<Direction> possibleDirections = new List<Direction> { Direction.up, Direction.down, Direction.left, Direction.right };
            int predators = 0;

            if (Game.SeekForMouse(x, y, Direction.up, predator, 1) > 0)
            {
                possibleDirections.Remove(Direction.up);
                predators++;
            }
            if (Game.SeekForMouse(x, y, Direction.down, predator, 1) > 0)
            {
                possibleDirections.Remove(Direction.down);
                predators++;
            }
            if (Game.SeekForMouse(x, y, Direction.left, predator, 1) > 0)
            {
                possibleDirections.Remove(Direction.left);
                predators++;
            }
            if (Game.SeekForMouse(x, y, Direction.right, predator, 1) > 0)
            {
                possibleDirections.Remove(Direction.right);
                predators++;
            }

            if (possibleDirections.Count > 0 && predators > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];
                return moveDirection;
            }
            else
            {
                return Direction.stay;
            }
        }

        // from Valentina
        public bool TotalFlee(int x, int y, string predator)
        {
            // make the first move
            Random random = new Random();
            Direction move = Flee(x, y, predator);
            if (Game.SeekForMouse(x, y, move, predator, 1) == 0)
            {
                Game.Move(this, move, 1);
            }

            // make sure the prey will not flee back to the original suqare
            List<Direction> possibleDirections = new List<Direction> { Direction.up, Direction.down, Direction.left, Direction.right };
            if (move == Direction.up)
            {
                possibleDirections.Remove(Direction.down);
            }
            else if (move == Direction.down)
            {
                possibleDirections.Remove(Direction.up);
            }
            else if (move == Direction.left)
            {
                possibleDirections.Remove(Direction.right);
            }
            else if (move == Direction.right)
            {
                possibleDirections.Remove(Direction.left);
            }

            // if the mouse can countinue moving, the direction will not change,
            // if cannot, it will choose a possible direction randomly.
            if (Game.SeekForMouse(x, y, move, predator, 2) == 0)
            {
                if (Game.Move(this, move, 1) > 0) return true;
            }
            else if (possibleDirections.Count > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];
                if (Game.Move(this, moveDirection, 1) > 0) return true;
            }
            return false;
        }
    }
}