using System;
using UnityEngine;

public class ConstantAccess: MonoBehaviour
{
        private static ConstantAccess _instance;
        public static ConstantAccess Instance => _instance;

        [SerializeField]private Camera _camera;
        [SerializeField] private GameObject _board;

        public Camera Camera => _camera;
        public GameObject Board => _board;

        private void Awake()
        {
                _instance = this;
        }
}