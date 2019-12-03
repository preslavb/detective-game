using System;
using Doozy.Engine.Nody.Models;
using Model.BoardItemModels;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using View.Interfaces;

namespace DesignData
{
    [CreateAssetMenu(order = -1, fileName = "Item Design Data", menuName = "Item Design Data")]
    public partial class DesignDataScriptableObject : SerializedScriptableObject
    {
        public enum DesignDataType
        {
            Event,
            Evidence,
            Resource
        }

        [PropertyOrder(-1)]
        [ShowInInspector]
        private string Name
        {
            get { return name; }

            set { this.name = value; }
        }
        
        [OdinSerialize]
        [EnumToggleButtons]
        private DesignDataType _dataType;

        public DesignDataType DataType => _dataType;
        
        [Space(10)]
        [InlineProperty]
        [SerializeField]
        public IBoardItemViewProperties ViewProperties;

        private BoardItemSerializable _modelData;
    }
}
