using System;
using System.Collections;
using DG.Tweening;
using Doozy.Engine;
using Doozy.Engine.Soundy;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace View.Scripts.Events
{
    [Serializable]
    public class DialogueScreenProperties
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshPro;

        [SerializeField] private UnityEvent _unityEvent;

        public TextMeshProUGUI TextMeshPro => _textMeshPro;

        public UnityEvent UnityEvent => _unityEvent;
    }
    
    public class DialogueControllerScript: MonoBehaviour
    {
        [SerializeField] 
        private ScrollRect _scrollView;

        [SerializeField] 
        private DialogueScreenProperties[] _slides;

        [SerializeField] private Image _backgroundImage;

        [SerializeField] 
        private Image _continueArrow;

        [SerializeField] private AudioMixerGroup _musicMixer;

        [SerializeField] private AudioSource _typewriterAudio;

        [SerializeField] private AudioClip _typewriterDingClip;

        [ShowInInspector]
        public float TextSpeed
        {
            get => 1 / _textDelay;
            set => _textDelay = 1 / value;
        }

        [HideInInspector]
        [SerializeField] private float _textDelay;


        private Coroutine _writingCoroutine;

        private bool _writingOut = false;
        private int _textIndex;

        private string _currentText = "";

        private void Awake()
        {
            // Make sure all of the texts are disabled at start
            foreach (var text in _slides)
            {
                text.TextMeshPro.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            StartWritingNextText();
            _musicMixer.audioMixer.SetFloat("MusicVolume", -20);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                if (_writingOut)
                {
                    CompleteCurrentText();
                }

                else if (_textIndex < _slides.Length)
                {
                    StartWritingNextText();
                }

                else
                {
                    GameEventMessage.SendEvent("Finished Dialogue");
                    _musicMixer.audioMixer.SetFloat("MusicVolume", 0);
                    Destroy(this);
                }
            }
        }

        private void StartWritingNextText()
        {
            // Remove the layout element from the previous object
            if (_textIndex > 0)
                Destroy(_slides[_textIndex - 1].TextMeshPro.gameObject.GetComponent<LayoutElement>());
            
            var currentTextObject = _slides[_textIndex];

            // Run any events
            currentTextObject.UnityEvent?.Invoke();
            
            // Enable the current text object
            currentTextObject.TextMeshPro.gameObject.SetActive(true);
            
            // Add the layout element constraints
            var layoutElement = currentTextObject.TextMeshPro.gameObject.AddComponent<LayoutElement>();
            layoutElement.minHeight = _scrollView.gameObject.GetComponent<RectTransform>().rect.height - 20;
            layoutElement.flexibleHeight = 1;
            
            // Extract the text from it
            _currentText = currentTextObject.TextMeshPro.text;
            currentTextObject.TextMeshPro.text = "";
            
            // Make sure the continue arrow is hidden
            _continueArrow.gameObject.SetActive(false);
            
            // Disable scrolling
            _scrollView.Rebuild(CanvasUpdate.Layout);
            _scrollView.verticalNormalizedPosition = 0;
            _scrollView.vertical = false;
            _scrollView.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
            _scrollView.verticalScrollbar.gameObject.SetActive(false);

            // Start the coroutine
            _writingCoroutine = StartCoroutine(WriteOutTextCoroutine());
        }

        private void CompleteCurrentText()
        {
            // Stop the coroutine
            StopCoroutine(_writingCoroutine);

            // Play the sound effect
            _typewriterAudio.PlayOneShot(_typewriterDingClip);
                    
            // Set the text of the current textMeshPro
            _slides[_textIndex].TextMeshPro.text = _currentText;

            // Flip the flag
            _writingOut = false;
            
            // Enable the arrow
            _continueArrow.gameObject.SetActive(true);
            
            // Enable scrolling
            _scrollView.vertical = true;
            _scrollView.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            _scrollView.verticalScrollbar.gameObject.SetActive(false);
            
            // Increment the current index
            _textIndex++;
        }

        private IEnumerator WriteOutTextCoroutine()
        {
            var textObject = _slides[_textIndex];
            var currentTextEnumerator = _currentText.GetEnumerator();

            _writingOut = true;

            for (var i = currentTextEnumerator; i.MoveNext();)
            {
                textObject.TextMeshPro.text = $"{textObject.TextMeshPro.text}{i.Current}";
                
                if (!_typewriterAudio.isPlaying)
                    _typewriterAudio.Play();
                
                yield return new WaitForSeconds(_textDelay);
            }
            
            _writingOut = false;
            
            CompleteCurrentText();
        }
    }
}