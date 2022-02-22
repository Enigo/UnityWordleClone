using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameField : MonoBehaviour
    {
        private Line[] _lines;
        private ErrorPopup _errorPopup;
        private GameData _gameData;
        private int _currentLine;
        private bool _gameEnd;
        private Statistic _statistic;

        private void Awake()
        {
            _lines = GetComponentsInChildren<Line>();
            _errorPopup = GetComponentInChildren<ErrorPopup>();
            _gameData = FindObjectOfType<GameData>();
            _statistic = GetComponentInChildren<Statistic>();
        }

        private void Start()
        {
            if (PlayerPrefsUtils.WasPlayedToday())
            {
                GetComponentsInChildren<Image>(true).Last().gameObject.SetActive(true);
            }
        }

        public bool IsValidInput()
        {
            var validationResult = _lines[_currentLine].IsValidInput(_gameData.GetWords());
            var valid = "".Equals(validationResult);

            if (!valid)
            {
                _errorPopup.ShowPopup(validationResult);
            }

            return valid;
        }

        public void CheckAnswer()
        {
            if (_gameEnd) return;

            var line = _lines[_currentLine];
            var word = _gameData.GetWord();
            var isValid = line.CheckAnswer(word);
            if (isValid)
            {
                _gameEnd = true;

                StartCoroutine(DelayedSuccess());

                IEnumerator DelayedSuccess()
                {
                    yield return new WaitForSeconds(Line.TransactionDuration * 7f * 2f);
                    line.PlaySuccess();
                    yield return new WaitForSeconds(Line.TransactionDuration * 3f);
                    _statistic.ShowOnWin(word, _currentLine);
                }
            }
            else
            {
                if (++_currentLine == _lines.Length)
                {
                    _gameEnd = true;

                    StartCoroutine(DelayedFailure());

                    IEnumerator DelayedFailure()
                    {
                        yield return new WaitForSeconds(Line.TransactionDuration * 7f * 2f);
                        _statistic.ShowOnFailure(word);
                    }
                }
            }
        }

        public void EnterLetter(LetterControl control)
        {
            if (_gameEnd) return;

            _lines[_currentLine].EnterLetter(control);
        }

        public void RemoveLastLetter()
        {
            if (_gameEnd) return;

            _lines[_currentLine].RemoveLast();
        }
    }
}