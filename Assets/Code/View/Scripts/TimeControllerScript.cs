using UnityEngine;
using UnityEngine.UI;

namespace View.Scripts
{
    public class TimeControllerScript: MonoBehaviour
    {
        [SerializeField] private Toggle _pause;
        [SerializeField] private Toggle _play;
        [SerializeField] private Toggle _fast;

        private void Start()
        {
            Play(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameTime.Instance.TimeScale == 0)
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
            GameTime.Instance.TimeScale = 0;

            if (active) _pause.isOn = true;
        }

        public void Play(bool active)
        {
            GameTime.Instance.TimeScale = 1;

            if (active) _play.isOn = true;
        }

        public void Fast(bool active)
        {
            GameTime.Instance.TimeScale = 2;

            if (active) _fast.isOn = true;
        }
    }
}