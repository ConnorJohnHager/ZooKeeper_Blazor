using System;

namespace ZooKeeper_Blazor
{
	public interface IPrey
	{
        // from Connor, based on code from Valentina
        public bool Flee(Animal prey, int x, int y, string predator, int distance)
        {
            Random random = new Random();
            List<Direction> possibleDirections = new List<Direction> {Direction.up, Direction.down, Direction.left, Direction.right};

            if (!Game.Seek(x, y, Direction.up, predator, 1))
            {
                possibleDirections.Remove(Direction.up);
            }
            if (!Game.Seek(x, y, Direction.down, predator, 1))
            {
                possibleDirections.Remove(Direction.down);
            }
            if (!Game.Seek(x, y, Direction.left, predator, 1))
            {
                possibleDirections.Remove(Direction.right);
            }
            if (!Game.Seek(x, y, Direction.right, predator, 1))
            {
                possibleDirections.Remove(Direction.left);
            }

            for ( int i = distance; i < 1; i++)
            {
                foreach (Direction direction in possibleDirections)
                {
                    if(!Game.Seek(x, y, direction, "null", distance))
                    {
                        possibleDirections.Remove(direction);
                    }
                }
            }

            if (possibleDirections.Count > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];

                if (Game.Move(prey, moveDirection, distance) > 0) return true;
            }
            return false;
        }
    }
}

