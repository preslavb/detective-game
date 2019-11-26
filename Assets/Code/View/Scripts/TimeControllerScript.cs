using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View.Scripts
{
    public class TimeControllerScript: MonoBehaviour
    {
        public delegate void ChangedTimeScaleDelegate(float newTimescale);
        
        [SerializeField] private Toggle _pause;
        [SerializeField] private Toggle _play;
        [SerializeField] private Toggle _fast;

        public event ChangedTimeScaleDelegate ChangedTimescale;

        private void Start()
        {
            Play(true);
        }

        public void UpdateInput(float newTimeScale)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (newTimeScale == 0)
                {
                    Play(true);
                }
                else
                {
                    Pause(true);
                }
            }

        }

        public void Pause(bool active)
        {
            ChangedTimescale?.Invoke(0);

            if (active) _pause.isOn = true;
        }

        public void Play(bool active)
        {
            ChangedTimescale?.Invoke(1);

            if (active) _play.isOn = true;
        }

        public void Fast(bool active)
        {
            ChangedTimescale?.Invoke(2);

            if (active) _fast.isOn = true;
        }
    }
}