using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DistributionData : MonoBehaviour
    {
        private TextMeshProUGUI _count;
        private Image _bar;

        private void Awake()
        {
            _count = GetComponentsInChildren<TextMeshProUGUI>()[1];
            _bar = GetComponentInChildren<Image>();
        }

        public void Setup(int count, bool toColor)
        {
            _count.text = count.ToString();
            _bar.color = toColor ? ColorCollection.Green : ColorCollection.Grey;
        }

        public void SetSize(int width)
        {
            _bar.rectTransform.sizeDelta = new Vector2(width, 100);
        }
    }
}