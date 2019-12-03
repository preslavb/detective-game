using Doozy.Engine.Nody.Models;
using UnityEngine;
using View.Interfaces;
using View.ViewDataClasses;

namespace DesignData
{
    public partial class DesignDataScriptableObject
    {
        [SerializeField]
        [HideInInspector]
        public Graph EventGraph;
    }
}