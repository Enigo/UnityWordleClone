using System.Collections;
using Game;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class LetterControlTest
    {
        private LetterControl _letterControl;
        private Image _image;
        private TextMeshProUGUI _text;
        private Controls _controls;

        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject();
            _image = gameObject.AddComponent<Image>();

            var childObject = new GameObject();
            _text = childObject.AddComponent<TextMeshProUGUI>();
            childObject.transform.SetParent(gameObject.transform);

            var parentObject = new GameObject();
            _controls = parentObject.AddComponent<Controls>();

            gameObject.transform.SetParent(parentObject.transform);

            _letterControl = gameObject.AddComponent<LetterControl>();
            _letterControl.Awake();
        }

        [Test]
        public void WhenGetLetterIsCalledValueIsReturned()
        {
            const string letter = "w";
            _text.text = letter;

            Assert.AreEqual(letter, _letterControl.GetLetter());
        }

        [UnityTest]
        public IEnumerator WhenChangeColorControlToGreenThenItIsChanged()
        {
            var colorToChange = ColorCollection.Green;
            _letterControl.ChangeColorControl(colorToChange);

            yield return new WaitForSeconds(0.15f * 11f);
            
            Assert.True(Mathf.Approximately(colorToChange.r, _image.color.r));
            Assert.True(Mathf.Approximately(colorToChange.g, _image.color.g));
            Assert.True(Mathf.Approximately(colorToChange.b, _image.color.b));
            Assert.True(Mathf.Approximately(colorToChange.a, _image.color.a));
        }
    }
}