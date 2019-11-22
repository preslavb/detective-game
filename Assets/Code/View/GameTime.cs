using UnityEngine;

namespace View
{
    public class GameTime: MonoBehaviour
    {
        private float _deltaTime = 0;
        
        public float DeltaTime => _deltaTime;
        public float TimeScale { get; set; } = 1;

        private static GameTime _instance = null;

        public static GameTime Instance => _instance;

        private void Start()
        {
            _instance = this;
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime * TimeScale;
        }
    }
}