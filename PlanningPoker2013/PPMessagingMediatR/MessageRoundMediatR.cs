using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using MediatR;
using PPObjects;

namespace PPMessagingMediatR
{
    public class MessageRoundMediatR : MessageRound, IAsyncNotification
    {
        public MessageRoundMediatR()
        {
            
        }
        public MessageRoundMediatR(MessageRound messageRound)
        {

            RoundName = messageRound.RoundName;
            RoundAction = messageRound.RoundAction;
        }
    }
}