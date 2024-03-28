using System;
namespace ZooKeeper_Blazor 
{
    // from Sunny
	public class ScoreCalculator : IScoreCalculator
    {
        public int CalculateTotalScore(List<List<Zone>> animalZones)
        {
            int numberOfZones;
            int numberOfTypes;
            int numberOfTypeLeastUsed;
            int numberOfTypeMostUsed;
            int totalScore;

            //The SelectMany function flattens the inner lists into a single sequence, and then we use Count() on this sequence to obtain the total count.
            numberOfZones = animalZones.SelectMany(innerlist => innerlist).Count();
            Console.WriteLine("numberOfZones" + numberOfZones);

            numberOfTypes = CountSpecies(animalZones);
            Console.WriteLine("numberOfTypes" + numberOfTypes);

            numberOfTypeLeastUsed = FindTheLeastOne(animalZones);
            Console.WriteLine("numberOfTypeLeastUsed" + numberOfTypeLeastUsed);

            numberOfTypeMostUsed = FindTheMostOne(animalZones);
            Console.WriteLine("numberOfTypeMostUsed" + numberOfTypeMostUsed);

            totalScore = (numberOfZones * numberOfTypes * numberOfTypeLeastUsed) / numberOfTypeMostUsed;
            return totalScore;
        }

        //This function is counting how many species are stay in the current zone
        public int CountSpecies(List<List<Zone>> animalZones)
        {
            List<string> animalTypeInCurrentZones = new List<string>();
            //recurring the current zone, if any of the zone is empty, which means that it is not full of animal, return
            for (int i = 0; i < animalZones.Count; i++)
            {
                for (int j = 0; j < animalZones[i].Count; j++)
                {
                    if (animalZones[i][j].occupant != null)
                    {
                        if (animalTypeInCurrentZones.Contains(animalZones[i][j].occupant.species)) continue;
                        else animalTypeInCurrentZones.Add(animalZones[i][j].occupant.species);
                    }
                }
            }
            return animalTypeInCurrentZones.Count;
        }

        //This function is to find in the current zone, which specie's number is the smallest
        public int FindTheLeastOne(List<List<Zone>> animalZones)
        {
            int currrentType;

            int[] numberOfEachType = new int[8];
            for (int i = 0; i < animalZones.Count; i++)
            {
                for (int j = 0; j < animalZones[i].Count; j++)
                {
                    if (animalZones[i][j].occupant != null)
                    {
                        switch (animalZones[i][j].occupant.species)
                        {
                            case "chick":
                                numberOfEachType[0]++;
                                break;
                            case "mouse":
                                numberOfEachType[1]++;
                                break;
                            case "cat":
                                numberOfEachType[2]++;
                                break;
                            case "raptor":
                                numberOfEachType[3]++;
                                break;
                            case "rooster":
                                numberOfEachType[4]++;
                                break;
                            case "vulture":
                                numberOfEachType[5]++;
                                break;
                            case "grass":
                                numberOfEachType[6]++;
                                break;
                            case "corpse":
                                numberOfEachType[7]++;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            //Filtering all values that equals zero
            var nonZeroValues = numberOfEachType.Where(val => val != 0).ToArray();

            //Return the minimum value after filtering
            return nonZeroValues.Min();
        }

        public int FindTheMostOne(List<List<Zone>> animalZones)
        {

            int[] numberOfEachType = new int[8];
            for (int i = 0; i < animalZones.Count; i++)
            {
                for (int j = 0; j < animalZones[i].Count; j++)
                {
                    if (animalZones[i][j].occupant != null)
                    {
                        switch (animalZones[i][j].occupant.species)
                        {
                            case "chick":
                                numberOfEachType[0]++;
                                break;
                            case "mouse":
                                numberOfEachType[1]++;
                                break;
                            case "cat":
                                numberOfEachType[2]++;
                                break;
                            case "raptor":
                                numberOfEachType[3]++;
                                break;
                            case "rooster":
                                numberOfEachType[4]++;
                                break;
                            case "vulture":
                                numberOfEachType[5]++;
                                break;
                            case "grass":
                                numberOfEachType[6]++;
                                break;
                            case "corpse":
                                numberOfEachType[7]++;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            Array.Sort(numberOfEachType);
            Array.Reverse(numberOfEachType);

            return numberOfEachType[0];
        }
    }
}

