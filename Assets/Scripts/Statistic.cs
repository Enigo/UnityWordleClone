using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game;
using GameState;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Statistic : MonoBehaviour
{
    private Image _answerImage;
    private TextMeshProUGUI _answer, _played, _win, _currentStreak, _maxStreak, _finishedText;
    private Button _playButton;
    private DistributionData[] _distributionDataLines;
    private GameStatsSaver _gameStatsSaver;
    private GameData _gameData;

    private Stats _stats;

    private void Awake()
    {
        _answerImage = GetComponentsInChildren<Image>()[1];
        var texts = GetComponentsInChildren<TextMeshProUGUI>();
        _answer = texts[0];
        _played = texts[2];
        _win = texts[3];
        _currentStreak = texts[4];
        _maxStreak = texts[5];
        _distributionDataLines = GetComponentsInChildren<DistributionData>();
        _playButton = GetComponentInChildren<Button>();
        _finishedText = GetComponentsInChildren<TextMeshProUGUI>(true).Last();

        _gameStatsSaver = FindObjectOfType<GameStatsSaver>();
        _gameData = FindObjectOfType<GameData>();
    }

    public void ShowOnWin(string answer, int currentLine)
    {
        _answer.text = answer;
        _answerImage.color = ColorCollection.Green;
        _stats = _gameStatsSaver.SaveSuccessStats(currentLine);

        CheckWordsCount();

        ExecuteOnShow(_stats, currentLine);
    }

    public void ShowOnFailure(string answer)
    {
        _answer.text = answer;
        _answerImage.color = ColorCollection.Grey;
        _stats = _gameStatsSaver.SaveFailureStats();

        CheckWordsCount();

        ExecuteOnShow(_stats);
    }

    private void CheckWordsCount()
    {
        if (_gameStatsSaver.GetCurrentWordIndex() >= _gameData.GetWords().Count)
        {
            _playButton.gameObject.SetActive(false);
            _finishedText.gameObject.SetActive(true);
        }
    }

    private void ExecuteOnShow(Stats savedStats, int currentLine = -1)
    {
        var totalPlayed = savedStats.failures + savedStats.successes;
        _played.text = totalPlayed.ToString();
        var winRate = totalPlayed == 0 ? 0 : savedStats.successes / (float) totalPlayed * 100;
        _win.text = Mathf.RoundToInt(winRate).ToString();
        _currentStreak.text = savedStats.currentStreak.ToString();
        _maxStreak.text = savedStats.maxStreak.ToString();

        // collecting all counts first
        var dic = new SortedDictionary<int, List<int>>();
        for (var i = 0; i < savedStats.lineSuccessStats.Length; i++)
        {
            var countForLine = savedStats.lineSuccessStats[i];
            _distributionDataLines[i].Setup(countForLine, currentLine == i);

            if (dic.ContainsKey(countForLine))
            {
                dic[countForLine].Add(i);
            }
            else
            {
                dic.Add(countForLine, new List<int> {i});
            }
        }

        // setting from the biggest to smallest
        var sizes = new[] {1000, 800, 600, 300, 140};
        var sizeIterator = 0;
        foreach (var (count, value) in dic.Reverse())
        {
            if (count == 0)
            {
                continue;
            }

            foreach (var i in value)
            {
                _distributionDataLines[i].SetSize(sizes[sizeIterator]);
            }

            sizeIterator++;
        }

        transform.DOLocalMoveY(0, 1.25f).SetEase(Ease.OutBack);
    }

    public void OnPlay()
    {
        SceneManager.LoadSceneAsync("Game");
    }
}