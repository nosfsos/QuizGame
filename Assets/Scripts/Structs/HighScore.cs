using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public class HighScore
    {
        [SerializeField]
        private List<Score> AllScores;

        public HighScore()
        {
            AllScores = new List<Score>();
        }

        public void AddScore(string name, string value)
        {
            var score = new Score() { Name = name, Value = value };
            AllScores.Add(score);
            AllScores = AllScores.OrderByDescending(sc => sc.Value).ToList();
        }

        public void RemoveScore(string name, string value)
        {
            try
            {
                AllScores.Remove(AllScores.First(score => score.Name == name && score.Value == value));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void RemoveScore(Score score)
        {
            if (AllScores.Contains(score))
                AllScores.Remove(score);
        }

        public IEnumerable<Score> GetTopScores(int n)
        {
            return AllScores.Take(n);
        }
    }


    [Serializable]
    public class Score
    {
        public string Name;
        public string Value;
    }
}