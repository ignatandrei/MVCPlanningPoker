using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PPObjects;

namespace PPMessagingMediatR
{
    public class MessageCardMediatR : MessageCard, IAsyncNotification
    {
        public MessageCardMediatR()
        {

        }

        public MessageCardMediatR(MessageCard message)
        {

            Card = message.Card;
            ParticipantName = message.ParticipantName;
        }
    }
}
