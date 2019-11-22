using System;
using Model.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class EventDecision
    {
        [SerializeField]
        private string _name;

        [ShowInInspector]
        [SerializeField]
        private BoardItemSerializable[] _outcomes;

        public string Name => _name;
    }
}