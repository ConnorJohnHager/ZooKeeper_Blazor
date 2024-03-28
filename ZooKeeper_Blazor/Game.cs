using System;
using System.Collections.Generic;
using System.Text;

namespace ZooKeeper_Blazor
{
    public static class Game
    {
        static public int numCellsX = 4;
        static public int numCellsY = 4;

        //Changing to public because ZoneManager needs these propeties
        static public int maxCellsX = 10;
        static public int maxCellsY = 10;


        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen = new Zone(-1, -1, null);
        static public int totalScore = 0;

        //New attributes, which will be used by ZoneManager
        static public int directionIndex;
        static public string direction = "";


        static public void SetUpGame()
        {
            ZoneManager zoneManager = new ZoneManager();
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
            //At the beginning of the game create a random direction
            direction = zoneManager.CreateRandomDirection();
        }

       //Since there is no need to add zones manually, so addzone will be deleted

        static public void ZoneClick(Zone clickedZone)
        {
            ScoreCalculator scoreCalculator = new ScoreCalculator();
            ZoneManager zoneManager = new ZoneManager();
            Console.Write("Got animal ");
            Console.WriteLine(clickedZone.emoji == "" ? "none" : clickedZone.emoji);
            Console.Write("Held animal is ");
            Console.WriteLine(holdingPen.emoji == "" ? "none" : holdingPen.emoji);
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                // take animal from zone to holding pen
                Console.WriteLine("Taking " + clickedZone.emoji);
                holdingPen.occupant = clickedZone.occupant;
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                clickedZone.occupant = null;
                ActivateAnimals();
                totalScore = scoreCalculator.CalculateTotalScore(animalZones);

                zoneManager.AddZoneWhenFull();//Adding new zone should be executed after all animals finish their actions
                if (zoneManager.IsWin())
                {
                    Console.WriteLine("Player reached the goal");
                    return;
                }
            }
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                ActivateAnimals();
                totalScore = scoreCalculator.CalculateTotalScore(animalZones);

                zoneManager.AddZoneWhenFull();//Adding new zone should be excute after all animals finished their actions
                if (zoneManager.IsWin())
                {
                    Console.WriteLine("Player reached the goal");
                    return;
                }
            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
        }

        static public void AddToHolding(string occupantType)
        {
            ZoneManager zoneManager = new ZoneManager();
            if (holdingPen.occupant != null) return;
            if (occupantType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (occupantType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (occupantType == "raptor") holdingPen.occupant = new Raptor("Chance the Raptor");
            if (occupantType == "chick") holdingPen.occupant = new Chick("Tweety (uncopyrighted)");
            if (occupantType == "rooster") holdingPen.occupant = new Rooster("Earl");
            if (occupantType == "vulture") holdingPen.occupant = new Vulture("Van Helswing");
            if (occupantType == "grass") holdingPen.occupant = new Grass();
            if (occupantType == "corpse") holdingPen.occupant = new Corpse();
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            //ActivateAnimals(); turns only occur when placed on the board now
            zoneManager.AddZoneWhenFull();//Keeping watching whether current is full and then adding new zone
        }

        static public void ActivateAnimals()
        {
            //Going through activations
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        if (zone.occupant as Animal != null && ((Animal)zone.occupant).reactionTime == r && ((Animal)zone.occupant).TurnCheck == false)
                        {
                            ((Animal)zone.occupant).Activate();
                        }
                    }
                }
            }

            //Corpses turn into grass after three turns
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    var zone = animalZones[y][x];

                    Corpse corpse = zone.occupant as Corpse;

                    if (corpse != null)
                    {
                        corpse.turnsRemaining--;
                        if (corpse.turnsRemaining == 0)
                        {
                            zone.occupant = new Grass();
                        }
                    }
                }
            }

            //Going through resetting turnchecks
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    var zone = animalZones[y][x];
                    if (zone.occupant as Animal != null)
                    {
                        ((Animal)zone.occupant).TurnCheck = false;
                    }
                }
            }
        }

        // updated by Connor
        static public bool Seek(int x, int y, Direction d, string target, int distance)
        {
            if (target == "null") // Searching for an empty spot
            {
                switch (d)
                {
                    case Direction.up:
                        y = y - distance;
                        break;
                    case Direction.down:
                        y = y + distance;
                        break;
                    case Direction.left:
                        x = x - distance;
                        break;
                    case Direction.right:
                        x = x + distance;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return true;
            }
            else
            {
                switch (d)
                {
                    case Direction.up:
                        y = y - distance;
                        break;
                    case Direction.down:
                        y = y + distance;
                        break;
                    case Direction.left:
                        x = x - distance;
                        break;
                    case Direction.right:
                        x = x + distance;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return false;
                if (animalZones[y][x].occupant.species == target)
                {
                    return true;
                }
            }
            return false;
        }

        // Original code from Valentina, updated by Connor
        static public int Move(Animal animal, Direction d, int distance)
        {
            int movedDistance = 0;
            int x = animal.location.x;
            int y = animal.location.y;

            for (int i = 0; i < distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) break;
                if (animalZones[y][x].occupant == null)
                {
                    animalZones[animal.location.y][animal.location.x].occupant = null;
                    animalZones[y][x].occupant = animal;
                    movedDistance++;
                }
                else
                {
                    break;
                }
            }
            return movedDistance;
        }

        // from Connor
        static public void Replace(int x, int y, Occupant newOccupant)
        {
            var zone = animalZones[y][x];
            zone.occupant = newOccupant;
        }

        // updated by Connor
        static public bool Attack(Animal attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    if (animalZones[y - 1][x].occupant != null)
                    {
                        animalZones[y - 1][x].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.down:
                    if (animalZones[y + 1][x].occupant != null)
                    {
                        animalZones[y + 1][x].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.left:
                    if (animalZones[y][x - 1].occupant != null)
                    {
                        animalZones[y][x - 1].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.right:
                    if (animalZones[y][x + 1].occupant != null)
                    {
                        animalZones[y][x + 1].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
            }
            return false; // nothing to hunt
        }

        // from Valentina
        static public int SeekForMouse(int x, int y, Direction d, string target, int distance)
        {
            int squaresToNearest = 0;
            for (int i = 1; i <= distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }

                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return 0;
                if (animalZones[y][x].occupant == null) return 0;
                if (animalZones[y][x].occupant.species == target)
                {
                    squaresToNearest = i;
                    return squaresToNearest;
                }
            }
            return 0;
        }
    }
}

