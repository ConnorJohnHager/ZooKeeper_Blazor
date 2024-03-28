using System;
using System.Collections.Generic;
using System.Text;

namespace ZooKeeper_Blazor
{
    /* 
    In the Games class, using the delegation mode to apply the function,
    Game will not directly realize the two interfaces. There will be two implementation classes to imply the interface.
    Game only needs to create an object of the implementation class and then use the function.
    */

    public static class Game
    {
        static public int numCellsX = 4;
        static public int numCellsY = 4;

        //Changing to public because ZoneManager needs these propeties
        static public int maxCellsX = 8;
        static public int maxCellsY = 8;


        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen = new Zone(-1, -1, null);
        static public int totalScore = 0;

        //New attributes, which will be used by ZoneManager
        static public int directionIndex;
        static public string direction;


        static public void SetUpGame()
        {
            //Creating the object of ZoneManager
            ZoneManager zoneManager = new ZoneManager();
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
            //At the beginning of the game create a random direction
            zoneManager.CreateRandomDirection();
        }

       //Since there is no need to add zones manually, so addzone will be deleted

        static public void ZoneClick(Zone clickedZone)
        {
            //Creating the object of ZoneManager and ScoreCalculator
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
                zoneManager.AddZoneWhenFull();//Adding new zone should be excute after all animals finished their actions
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

        static public void AddAnimalToHolding(string animalType)
        {
            ZoneManager zoneManager = new ZoneManager();
            if (holdingPen.occupant != null) return;
            if (animalType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (animalType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (animalType == "raptor") holdingPen.occupant = new Raptor("Chance the Raptor");
            if (animalType == "chick") holdingPen.occupant = new Chick("Tweety (uncopyrighted)");
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            ActivateAnimals();
            zoneManager.AddZoneWhenFull();//Keeping watching whether current is full and then adding new zone
        }

        static public void ActivateAnimals()
        {
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        if (zone.occupant != null && zone.occupant.reactionTime == r && zone.occupant.TurnCheck == false)
                        {
                            zone.occupant.Activate();
                        }
                    }
                }
            }
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    var zone = animalZones[y][x];
                    if (zone.occupant != null)
                    {
                        zone.occupant.TurnCheck = false;
                    }
                }
            }
        }

        static public bool Seek(int x, int y, Direction d, string target)
        {
            if (target == "null") // Searching for an empty spot
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
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return true;
            }
            else
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
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return false;
                if (animalZones[y][x].occupant.species == target)
                {
                    return true;
                }
            }
            return false;
        }

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

        static public bool Retreat(Animal runner, Direction d)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
            }
            return false; // cannot retreat
        }
    }
}

