using System;

// ReSharper disable InconsistentNaming
namespace GameState
{
    [Serializable]
    public class Stats
    {
        // index corresponds to line when puzzle was solved
        public int[] lineSuccessStats;
        public int currentWordIndex;
        public int successes;
        public int failures;
        public int currentStreak;
        public int maxStreak;
    }
}