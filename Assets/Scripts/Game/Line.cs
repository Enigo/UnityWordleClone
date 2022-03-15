using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Line : MonoBehaviour
    {
        public const float TransactionDuration = 0.15f;

        private Image[] _images;
        private TextMeshProUGUI[] _texts;
        private int _currentField;
        private LetterControl[] _letterControls;

        private void Awake()
        {
            _images = GetComponentsInChildren<Image>();
            _texts = GetComponentsInChildren<TextMeshProUGUI>();

            _letterControls = new LetterControl[_images.Length];
        }

        public string IsValidInput(List<string> allWords)
        {
            var errorMessage = "";
            for (var i = 0; i < _images.Length; i++)
            {
                if ("".Equals(_texts[i].text))
                {
                    _images[i].transform.DOKill(true);
                    _images[i].transform.DOShakePosition(0.75f, new Vector3(10f, 0));
                    errorMessage = "Not enough letters";
                }
            }

            var text = _texts.Aggregate("", (current, textMesh) => current + textMesh.text).ToLowerInvariant();

            if ("".Equals(errorMessage) && !allWords.Contains(text))
            {
                errorMessage = "Not in category word list";
                foreach (var image in _images)
                {
                    image.transform.DOKill(true);
                    image.transform.DOShakePosition(0.75f, new Vector3(10f, 0));
                }
            }

            return errorMessage;
        }

        public bool CheckAnswer(string word)
        {
            _currentField = 0;
            var text = _texts.Aggregate("", (current, textMesh) => current + textMesh.text).ToLowerInvariant();

            var colors = new Color[_texts.Length];
            var seenIndexes = new List<int>();
            var correctAnswers = new Dictionary<char, int>();

            for (var i = 0; i < _texts.Length; i++)
            {
                var inputChar = _texts[i].text[0];
                if (inputChar.Equals(word[i]))
                {
                    colors[i] = ColorCollection.Green;

                    if (correctAnswers.ContainsKey(inputChar))
                    {
                        correctAnswers[inputChar]++;
                    }
                    else
                    {
                        correctAnswers[inputChar] = 1;
                    }

                    seenIndexes.Add(i);
                }
                else if (!word.Contains(inputChar))
                {
                    colors[i] = ColorCollection.Grey;
                    seenIndexes.Add(i);
                }
            }

            for (var i = 0; i < _texts.Length; i++)
            {
                if (seenIndexes.Contains(i)) continue;

                var inputChar = _texts[i].text[0];

                colors[i] = ColorCollection.Yellow;

                var answerCountTotal = word.Count(c => c.Equals(inputChar));

                if (correctAnswers.ContainsKey(inputChar) && correctAnswers[inputChar] == answerCountTotal)
                {
                    colors[i] = ColorCollection.Grey;
                    continue;
                }

                var inputCountCurrent = 0;

                for (var j = 0; j < i; j++)
                {
                    var charInner = _texts[j].text[0];
                    if (charInner.Equals(inputChar) && ++inputCountCurrent >= answerCountTotal)
                    {
                        colors[i] = ColorCollection.Grey;
                        break;
                    }
                }
            }

            var delay = 0f;
            for (var i = 0; i < _images.Length; i++)
            {
                var image = _images[i];
                DOTween.Sequence()
                    .SetDelay(delay)
                    .Append(image.transform.DOScaleY(0, TransactionDuration))
                    .Append(image.DOColor(colors[i], 0))
                    .Append(_texts[i].DOColor(Color.white, 0))
                    .Append(image.transform.DOScaleY(1, TransactionDuration));

                _letterControls[i].ChangeColorControl(colors[i]);

                delay += TransactionDuration * 2;
            }

            return text.Equals(word);
        }

        public void PlaySuccess()
        {
            foreach (var image in _images)
            {
                image.transform.DOPunchPosition(new Vector3(0, 40), TransactionDuration * 2)
                    .SetEase(Ease.InOutCirc);
            }
        }

        public void EnterLetter(LetterControl control)
        {
            _images[_currentField].transform.DOPunchScale(new Vector3(0.15f, 0.15f), 0.2f, 1, 0);
            _letterControls[_currentField] = control;
            _texts[_currentField++].text = control.GetLetter();
        }

        public void RemoveLast()
        {
            _texts[--_currentField].text = "";
        }

        public IEnumerable<Color> GetAnswerColors()
        {
            return _images.Select(image => image.color).ToArray();
        }
    }
}