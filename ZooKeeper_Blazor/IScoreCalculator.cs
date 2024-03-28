﻿using System;
namespace ZooKeeper_Blazor
{
    // from Sunny
	public interface IScoreCalculator
	{
        int CalculateTotalScore(List<List<Zone>> animalZones);
        int CountSpecies(List<List<Zone>> animalZones);
        int FindTheLeastOne(List<List<Zone>> animalZones);
        int FindTheMostOne(List<List<Zone>> animalZones);
    }
}

