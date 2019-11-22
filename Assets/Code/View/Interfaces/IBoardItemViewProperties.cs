using Model.Interfaces;
using UnityEngine;

namespace View.Interfaces
{
    public interface IBoardItemViewProperties
    {
        GameObject Instantiate(Transform root, IBoardItem boardItem);
    }
}