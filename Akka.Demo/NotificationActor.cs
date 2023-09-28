using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Demo
{
    internal class NotificationActor : UntypedActor
    {
        private readonly IEmailNotification emailNotification;
        private readonly IActorRef childActor;

        public NotificationActor(IEmailNotification emailNotification) 
        {
            this.emailNotification = emailNotification;
            this.childActor = Context.ActorOf(Context.System.DI().Props<TextNotificationActor>());
        }
        protected override void OnReceive(object message)
        {
            Console.WriteLine($"Message received: {message}");
            emailNotification.Send(message?.ToString());
            childActor.Tell(message);
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            //Strategy for Actor which exception Occured we have 
            // All for one Strategy
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromMinutes(1),
                localOnlyDecider: ex =>
                {
                    return ex switch
                    {
                        ArgumentException ae => Directive.Resume,
                        NullReferenceException ne => Directive.Restart,
                        _ => Directive.Stop
                    };
                }
                );
        }

        protected override void PreStart() => Console.WriteLine("Actor Started");

        protected override void PostStop() => Console.WriteLine("Actor Stopped");  
    }
}
