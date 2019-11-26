using System;

namespace Model.Interfaces
{
    public interface IExpirable
    {
        float Timer { get; }
        float ExpirationTime { get; }

        void Initialize(ITickable gameTime);

        Action<float> OnTimerChange { get; set; }
        event Delegates.VoidDelegate OnExpire;
    }
}