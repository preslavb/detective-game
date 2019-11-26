namespace View.Interfaces
{
    public interface IExpirable
    {
        /// <param name="t">The time "fraction" left until expiration, Ranging between 0 and 1</param>
        void UpdateExpirable(float t);
    }
}