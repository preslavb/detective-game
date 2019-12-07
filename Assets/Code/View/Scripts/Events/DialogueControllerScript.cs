using System;
using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace View.Scripts.Events
{
    public class DialogueControllerScript: MonoBehaviour
    {
        [SerializeField] 
        private ScrollRect _scrollView;

        [SerializeField] 
        private TextMeshProUGUI[] _texts;

        [SerializeField] 
        private Image _continueArrow;

        [ShowInInspector]
        public float TextSpeed
        {
            get => 1 / _textDelay;
            set => _textDelay = 1 / value;
        }

        [HideInInspector]
        [SerializeField] private float _textDelay;

        private bool _writingOut = false;
        private int _textIndex;

        private string _currentText = "";

        private void Awake()
        {
            // Make sure all of the texts are disabled at start
            foreach (var textMeshProUgui in _texts)
            {
                textMeshProUgui.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            StartWritingNextText();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_writingOut)
                {
                    CompleteCurrentText();
                }

                else
                {
                    StartWritingNextText();
                }
            }
        }

        private void StartWritingNextText()
        {
            var currentTextObject = _texts[_textIndex];
            
            // Enable the current text object
            currentTextObject.gameObject.SetActive(true);
            
            // Extract the text from it
            _currentText = currentTextObject.text;
            currentTextObject.text = "";
            
            // Make sure the continue arrow is hidden
            _continueArrow.gameObject.SetActive(false);
            
            // Start the coroutine
            StartCoroutine(WriteOutTextCoroutine());
        }

        private void CompleteCurrentText()
        {
            // Stop the coroutine
            StopCoroutine(WriteOutTextCoroutine());
                    
            // Set the text of the current textMeshPro
            _texts[_textIndex].text = _currentText;
            
            // Enable the arrow
            _continueArrow.gameObject.SetActive(true);
            
            // Increment the current index
            _textIndex++;
        }

        private IEnumerator WriteOutTextCoroutine()
        {
            var textObject = _texts[_textIndex];
            var currentTextEnumerator = _currentText.GetEnumerator();

            _writingOut = true;

            for (var i = currentTextEnumerator; i.MoveNext();)
            {
                textObject.text = $"{textObject.text}{i.Current}";
                
                yield return new WaitForSeconds(_textDelay);
            }
            
            _writingOut = false;
            
            CompleteCurrentText();
        }
    }
}