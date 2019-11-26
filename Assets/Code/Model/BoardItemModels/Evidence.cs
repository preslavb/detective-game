using Model.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using View;

namespace Model.BoardItemModels
{
    [CreateAssetMenu(order = 0, fileName = "Evidence 1", menuName = "Board Item Data/Evidence")]
    public class Evidence: BoardItemSerializable
    {
        public override void Initialize(ITickable gameTime)
        {
        }
    }
}