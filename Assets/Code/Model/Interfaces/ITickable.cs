using System;

namespace Model.Interfaces
{
    public interface ITickable
    {
        void Tick(float systemDeltaTime);

        Action<float> OnTick { get; set; }
    }
}