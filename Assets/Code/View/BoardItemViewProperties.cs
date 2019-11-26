using System;
using Model.Interfaces;
using UnityEngine;
using View.Interfaces;

namespace View
{
    // TODO: Should be completely extracted out into controller
    [Serializable]
    public abstract class BoardItemViewProperties: IBoardItemViewProperties
    {
        public abstract GameObject Instantiate(Transform root, IBoardItem boardItem);
    }
}