using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;


namespace PPMessagingMediatR
{
    public class ReceiveMessageParticipant : 
        IAsyncNotificationHandler<MessageParticipantMediatR>, 
        IAsyncNotificationHandler<MessageRoundMediatR>,
        IAsyncNotificationHandler<MessageCardMediatR>

    {
        public string NameParticipant { get; set; }
        public List<object> messagesAdded { get; set; }
        
        public ReceiveMessageParticipant(string nameParticipant)
        {
            NameParticipant = nameParticipant;
            messagesAdded=new List<object>();
        }

        public async System.Threading.Tasks.Task Handle(MessageParticipantMediatR notification)
        {
            Console.WriteLine(NameParticipant + " received " + notification.ParticipantName +"--"+ notification.Action2Participant.ToString());
            messagesAdded.Add(notification);
            await Task.Delay(10);
        }

        public async Task Handle(MessageRoundMediatR notification)
        {
            Console.WriteLine(NameParticipant + " received " + notification.RoundName + "--" + notification.RoundAction.ToString());
            messagesAdded.Add(notification);
            await Task.Delay(10);
        }

        public async Task Handle(MessageCardMediatR notification)
        {
            Console.WriteLine(NameParticipant + " received " + notification.Card + "--" + notification.ParticipantName);
            messagesAdded.Add(notification);
            await Task.Delay(10);
        }
    }
}