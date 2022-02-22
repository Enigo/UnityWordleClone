using System.IO;
using UnityEngine;

namespace GameState
{
    public class GameStatsSaver : MonoBehaviour
    {
        private string _statsPath;

        private void Awake()
        {
            _statsPath = Path.Combine(Application.persistentDataPath, "st.dat");

            if (!File.Exists(_statsPath))
            {
                CreateNewStatsFile();
            }
        }

        public Stats SaveSuccessStats(int lineNumber)
        {
            var data = LoadStatsData();
            data.successes++;
            data.lineSuccessStats[lineNumber]++;
            data.currentStreak++;
            data.currentWordIndex++;
            if (data.currentStreak > data.maxStreak)
            {
                data.maxStreak = data.currentStreak;
            }

            SaveFile(_statsPath, data);

            return data;
        }

        public Stats SaveFailureStats()
        {
            var data = LoadStatsData();
            data.failures++;
            data.currentStreak = 0;
            data.currentWordIndex++;

            SaveFile(_statsPath, data);

            return data;
        }

        public int GetCurrentWordIndex()
        {
            return LoadStatsData().currentWordIndex;
        }

        private void CreateNewStatsFile()
        {
            SaveFile(_statsPath,
                new Stats
                {
                    lineSuccessStats = new int[6]
                });
        }

        private Stats LoadStatsData()
        {
            return JsonUtility.FromJson<Stats>(File.ReadAllText(_statsPath));
        }


        private static void SaveFile(string filePath, object data)
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(data));
        }
    }
}