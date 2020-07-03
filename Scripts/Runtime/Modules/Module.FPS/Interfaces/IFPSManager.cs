namespace Fiber
{
    public interface IFPSManager
    {
        int Current { get; }
        void Limit(int targetFPS);
        void Unlimit();
    }
}