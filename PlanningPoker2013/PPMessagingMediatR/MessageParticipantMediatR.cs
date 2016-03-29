using MediatR;
using PPObjects;

namespace PPMessagingMediatR
{
    public class MessageParticipantMediatR : MessageParticipant, IAsyncNotification
    {
        public MessageParticipantMediatR(MessageParticipant messageParticipant)
        {
            ParticipantName = messageParticipant.ParticipantName;
            Action2Participant = messageParticipant.Action2Participant;
        }
    }
}