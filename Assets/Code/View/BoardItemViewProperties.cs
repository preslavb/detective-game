using System;
using Model.Interfaces;
using UnityEngine;
using View.Interfaces;

namespace View
{
    [Serializable]
    public abstract class BoardItemViewProperties<T>: IBoardItemViewProperties where T: IBoardItem
    {
        public abstract GameObject Instantiate(Transform root, IBoardItem boardItem);
    }
}