using UnityEngine;

namespace View.Scripts.MouseHandler
{
    public interface IClickHandler
    {
        void Entered();
        void Escaped();
        IClickHandler HandleClicks(Camera camera);
    }
}