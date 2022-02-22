using System;
using System.Collections.Generic;
using System.Linq;
using GameState;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private List<string> _words = new List<string>();
    private int _currentTopicIndex;

    private void Awake()
    {
        ReadFile();
    }

    private void Start()
    {
        _currentTopicIndex = FindObjectOfType<GameStatsSaver>().GetCurrentWordIndex();
    }

    private void ReadFile()
    {
        var file = Resources.Load<TextAsset>("Words");
        if (file == null)
        {
            throw new ApplicationException("Words file is not accessible");
        }

        _words = file.text.Split('\n').Select(text => text.Trim()).ToList();
    }

    public List<string> GetWords()
    {
        return _words;
    }

    public string GetWord()
    {
        return GetWords()[_currentTopicIndex].ToLowerInvariant();
    }
}