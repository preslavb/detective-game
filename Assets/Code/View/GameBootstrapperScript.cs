using System;
using Controller;
using Model;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace View
{
    public class GameBootstrapperScript: SerializedMonoBehaviour
    {
        [OdinSerialize]
        [Required]
        private GameController _gameController;

        private void Start()
        {
            _gameController.Start();
        }

        private void Update()
        {
            _gameController.Update();
        }
    }
}