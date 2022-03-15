using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PopupModal : MonoBehaviour
    {
        private Image _image;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowPopup(string text)
        {
            _text.text = text;

            _image.DOColor(Color.black, 0.5f);
            _text.DOColor(Color.white, 0.51f).OnComplete(() =>
            {
                DOTween.Sequence()
                    .SetDelay(0.75f)
                    .Append(_image.DOColor(Color.clear, 0.5f))
                    .Join(_text.DOColor(Color.clear, 0.5f));
            });
        }
    }
}