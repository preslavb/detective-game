using System;
using Doozy.Engine.Nody.Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace DesignData
{
    [CreateAssetMenu(order = -1, fileName = "Item Design Data", menuName = "Item Design Data")]
    public class DesignDataScriptableObject : ScriptableObject
    {
        public Graph Test;
    }
}
