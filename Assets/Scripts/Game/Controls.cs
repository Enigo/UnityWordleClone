using UnityEngine;

namespace Game
{
    public class Controls : MonoBehaviour
    {
        private const int MaxValues = 5;

        private GameField _gameField;
        private int _valuesEntered;

        private void Awake()
        {
            _gameField = FindObjectOfType<GameField>();
        }

        public void CheckAnswer()
        {
            if (!_gameField.IsValidInput()) return;

            _gameField.CheckAnswer();
            _valuesEntered = 0;
        }

        public void RemoveLast()
        {
            if (_valuesEntered == 0) return;

            _gameField.RemoveLastLetter();

            _valuesEntered--;
        }

        public void EnterLetter(LetterControl control)
        {
            if (_valuesEntered >= MaxValues) return;

            _gameField.EnterLetter(control);

            _valuesEntered++;
        }
    }
}