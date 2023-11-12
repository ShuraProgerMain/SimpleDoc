using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SomeArrayGenerator
{
    private Random _random;
    private readonly string[] _rewardsKeysWithoutEnergy =
        { "Gold", "Apple", "Lottery", "RedOrBlack", "WheelOfFortune", "Clubs" };

    public SomeArrayGenerator()
    {
        _random = new Random();
    }
    
    private void SuperGamesNotNeighbors()
    {
            var superGamesPositions = new Queue<int>(_random.Next(0, 2) == 0
                ? new[] { 4, 2, 0 }
                : new[] { 5, 3, 1 });
        var superGames = new[] { "Lottery", "RedOrBlack", "WheelOfFortune" };
        superGames.ShuffleArray(_random);

        foreach (var superGame in superGames)
        {
            for (var i = 0; i < _rewardsKeysWithoutEnergy.Length; i++)
            {
                if (superGamesPositions.Count < 1)
                    break;
                
                var reward = _rewardsKeysWithoutEnergy[i];

                if (reward != superGame) continue;
                
                _rewardsKeysWithoutEnergy.SwapElementsInArray(i, superGamesPositions.Dequeue());
                break;
            }
        }
    }

    public string[] GetShuffledRewards()
    {
        _rewardsKeysWithoutEnergy.ShuffleArray(_random);
        SuperGamesNotNeighbors();
        
        return _rewardsKeysWithoutEnergy;
    }
}


public static class ArrayExtensions
{
    public static void ShuffleArray<T>(this T[] array, Random random)
    {
        for (var i = 0; i < array.Length; i++)
        {
            var r = random.Next(i, array.Length);
            (array[r], array[i]) = (array[i], array[r]);
        }
    }

    public static void SwapElementsInArray(this string[] array, int index1, int index2)
    {
        if (index1 < 0 || index1 >= array.Length || index2 < 0 || index2 >= array.Length || index1 == index2)
        {
            return;
        }

        (array[index1], array[index2]) = (array[index2], array[index1]);
    }
}
