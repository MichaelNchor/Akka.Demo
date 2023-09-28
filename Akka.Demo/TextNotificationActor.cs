using Akka.Actor;
namespace Akka.Demo
{
    public class TextNotificationActor : UntypedActor
    {
        protected override void PreStart() => Console.WriteLine("TextNotification child Started");
        protected override void PostStop() => Console.WriteLine("TextNotification child Stopped");
        protected override void OnReceive(object message)
        {
            if(message.ToString() == "n")
            {
                throw new NullReferenceException();
            }
            if (message.ToString() == "e")
            {
                throw new ArgumentException();
            }
            if (string.IsNullOrEmpty(message.ToString()))
            {
                throw new Exception();
            }
            Console.WriteLine($"Sending message {message}");
            //throw new Exception("text ex");
        }
    }
}
