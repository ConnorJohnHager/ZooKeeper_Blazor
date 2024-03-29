using System;

namespace ZooKeeper_Blazor
{
	public class Bird : Animal
	{
        // from Connor, based on code from Valentina
        public bool Fly(Bird bird, int x, int y, int distance)
        {
            Random random = new Random();
            List<Direction> possibleDirections = new List<Direction> {Direction.up, Direction.down, Direction.left, Direction.right};

            if (!Game.Seek(x, y, Direction.up, "null", distance))
            {
                possibleDirections.Remove(Direction.up);
            }
            if (!Game.Seek(x, y, Direction.down, "null", distance))
            {
                possibleDirections.Remove(Direction.down);
            }
            if (!Game.Seek(x, y, Direction.left, "null", distance))
            {
                possibleDirections.Remove(Direction.left);
            }
            if (!Game.Seek(x, y, Direction.right, "null", distance))
            {
                possibleDirections.Remove(Direction.right);
            }

            if (possibleDirections.Count > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];

                if (Game.Move(bird, moveDirection, distance) > 0) return true;
            }
            return false;
        }
    }
}

