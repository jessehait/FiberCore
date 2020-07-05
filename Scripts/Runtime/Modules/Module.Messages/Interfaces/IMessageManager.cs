using Fiber.Message;

namespace Fiber
{
    public interface IMessageManager
    {
        void Publish<T>(T message);
        IObservableMessage<T> Receive<T>();
    }
}