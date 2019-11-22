using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public interface IClickHandler
    {
        IClickHandler HandleClicks(Camera camera);
    }
}