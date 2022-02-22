using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LetterControl : MonoBehaviour
    {
        private Image _image;
        private TextMeshProUGUI _text;
        private Controls _controls;
        private ColorState _colorState = ColorState.None;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _controls = GetComponentInParent<Controls>();
        }

        public string GetLetter()
        {
            return _text.text.ToLowerInvariant();
        }

        public void EnterLetter()
        {
            _controls.EnterLetter(this);
        }

        public void ChangeColorControl(Color inputColor)
        {
            Color colorToChange;
            if (_colorState == ColorState.None)
            {
                if (ColorCollection.Green.Equals(inputColor))
                {
                    _colorState = ColorState.Green;
                    colorToChange = ColorCollection.Green;
                }
                else if (ColorCollection.Yellow.Equals(inputColor))
                {
                    _colorState = ColorState.Yellow;
                    colorToChange = ColorCollection.Yellow;
                }
                else
                {
                    _colorState = ColorState.Grey;
                    colorToChange = ColorCollection.Grey;
                }
            }
            else if (_colorState == ColorState.Green || ColorCollection.Green.Equals(inputColor))
            {
                colorToChange = ColorCollection.Green;
                _colorState = ColorState.Green;
            }
            else if (_colorState == ColorState.Yellow || ColorCollection.Yellow.Equals(inputColor))
            {
                colorToChange = ColorCollection.Yellow;
                _colorState = ColorState.Yellow;
            }
            else
            {
                colorToChange = ColorCollection.Grey;
                _colorState = ColorState.Grey;
            }

            DOTween.Sequence()
                .SetDelay(Line.TransactionDuration * 10)
                .Append(_image.DOColor(colorToChange, Line.TransactionDuration))
                .Join(_text.DOColor(Color.white, Line.TransactionDuration));
        }

        private enum ColorState
        {
            Green,
            Yellow,
            Grey,
            None
        }
    }
}