using System;

namespace ZooKeeper_Blazor
{
	public interface IPrey
	{
        public bool Flee(Animal prey, int x, int y, string predator)
        {
            if (Game.Seek(x, y, Direction.up, predator))
            {
                if (Game.Seek(x, y, Direction.up, "null")) // check all directions for fleeing
                {
                    if (Game.Retreat(prey, Direction.up)) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null"))
                {
                    if (Game.Retreat(prey, Direction.down)) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null"))
                {
                    if (Game.Retreat(prey, Direction.left)) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null"))
                {
                    if (Game.Retreat(prey, Direction.right)) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.down, predator))
            {
                if (Game.Seek(x, y, Direction.up, "null")) // check all directions for fleeing
                {
                    if (Game.Retreat(prey, Direction.up)) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null"))
                {
                    if (Game.Retreat(prey, Direction.down)) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null"))
                {
                    if (Game.Retreat(prey, Direction.left)) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null"))
                {
                    if (Game.Retreat(prey, Direction.right)) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.left, predator))
            {
                if (Game.Seek(x, y, Direction.up, "null")) // check all directions for fleeing
                {
                    if (Game.Retreat(prey, Direction.up)) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null"))
                {
                    if (Game.Retreat(prey, Direction.down)) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null"))
                {
                    if (Game.Retreat(prey, Direction.left)) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null"))
                {
                    if (Game.Retreat(prey, Direction.right)) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.right, predator))
            {
                if (Game.Seek(x, y, Direction.up, "null")) // check all directions for fleeing
                {
                    if (Game.Retreat(prey, Direction.up)) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null"))
                {
                    if (Game.Retreat(prey, Direction.down)) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null"))
                {
                    if (Game.Retreat(prey, Direction.left)) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null"))
                {
                    if (Game.Retreat(prey, Direction.right)) return true;
                }
                return false; // can't run
            }
            return false; // nothing to flee
        }
    }
}

