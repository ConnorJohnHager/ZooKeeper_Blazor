using System;

namespace ZooKeeper_Blazor
{
	public interface IPrey
	{
        // i'm not sure if your Flee() method is necessary for your part,
        // i'll just put my methods about flee below your code in this file
        public bool Flee(Animal prey, int x, int y, string predator, int distance)
        {
            if (Game.Seek(x, y, Direction.up, predator, 1))
            {
                if (Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null", 1))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null", 1))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null", 1))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.down, predator, 1))
            {
                if (Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null", 1))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null", 1))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null", 1))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.left, predator, 1))
            {
                if (Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null", 1))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null", 1))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null", 1))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.right, predator, 1))
            {
                if (Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null", 1))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null", 1))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null", 1))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            return false; // nothing to flee
        }

        // my code
        // get the first move direction
        public Direction Flee1(int x, int y, string predator)
        {
            Random random = new Random();
            List<Direction> possibleDirections = new List<Direction> { Direction.up, Direction.down, Direction.left, Direction.right };
            int predators = 0;

            if (Game.Seek(x, y, Direction.up, predator, 1))
            {
                possibleDirections.Remove(Direction.up);
                predators++;
            }
            if (Game.Seek(x, y, Direction.down, predator, 1))
            {
                possibleDirections.Remove(Direction.down);
                predators++;
            }
            if (Game.Seek(x, y, Direction.left, predator, 1))
            {
                possibleDirections.Remove(Direction.left);
                predators++;
            }
            if (Game.Seek(x, y, Direction.right, predator, 1))
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

        // total two steps of fleeing
        public void TotalFlee(Animal prey, int x, int y, string predator)
        {
            // make the first move
            Random random = new Random();
            Direction move = Flee1(x, y, predator);
            if (Game.Seek(x, y, move, predator, 1) == 0)
            {
                Game.Move(prey, move, 1);
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
            if (Game.Seek(x, y, move, predator, 2) == 0)
            {
                Game.Move(prey, move, 1);
            }
            else if (possibleDirections.Count > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];
                Game.Move(prey, moveDirection, 1);
            }
        }

        // will not use in mouse
        public bool Flee(Animal prey, int x, int y, string predator)
        {
            if (Game.Seek(x, y, Direction.up, predator, 1))
            {
                if (Game.Retreat(prey, Direction.down, 1))
                {
                    return true;
                }
            }
            if (Game.Seek(x, y, Direction.down, predator, 1))
            {
                if (Game.Retreat(prey, Direction.up, 1))
                {
                    return true;
                }
            }
            if (Game.Seek(x, y, Direction.left, predator, 1))
            {
                if (Game.Retreat(prey, Direction.right, 1))
                {
                    return true;
                }
            }
            if (Game.Seek(x, y, Direction.right, predator, 1))
            {
                if (Game.Retreat(prey, Direction.left, 1))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

