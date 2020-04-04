using System;
using System.Collections;
using Doozy.Engine.Soundy;
using UnityEngine;
using UnityEngine.Audio;

namespace View.Scripts
{
    public class MusicController: MonoBehaviour
    {
        private int _currentIndex = 0;
        
        [SerializeField] private AudioClip[] _music;

        [SerializeField] private AudioMixerGroup _mixerGroup;

        private SoundyController _currentController;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            while (true)
            {
                _currentController = SoundyManager.Play(_music[_currentIndex], _mixerGroup);
                
                yield return new WaitForSeconds(_music[_currentIndex].length + 5);
                
                _currentIndex = (_currentIndex + 1) % _music.Length;
            }
        }
    }
}