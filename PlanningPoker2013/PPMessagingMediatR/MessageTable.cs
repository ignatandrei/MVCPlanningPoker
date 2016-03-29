using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using PPObjects;

namespace PPMessagingMediatR
{
    

    
   
    public class MessageTableData
    {
        public TableData tableData;

        private MediatorTable mediatorTable;
        public List<ReceiveMessageParticipant> receiveMessageParticipants;
        
        public MessageTableData(string moderatorName)
        {
            tableData = new TableFactory().CreateTable(moderatorName);
            tableData.Table.ParticipantMessage += Table_ParticipantMessage;
            tableData.Table.MessageRound += Table_MessageRound;
            tableData.Table.CardChoosen += Table_CardChoosen;
            mediatorTable = new MediatorTable(this.MultiInstanceFactory);            
            receiveMessageParticipants=new List<ReceiveMessageParticipant>();
            receiveMessageParticipants.Add(new ReceiveMessageParticipant(tableData.Table.ModeratorName));


        }

        void Table_CardChoosen(object sender, MessageCard e)
        {
            mediatorTable.PublishAsync(new MessageCardMediatR(e)).GetAwaiter().GetResult();
        }

        void Table_MessageRound(object sender, MessageRound e)
        {
            switch (e.RoundAction)
            {
                case RoundAction.None:
                    throw new ArgumentException("message type action :" + e.RoundAction);
                default:
                    mediatorTable.PublishAsync(new MessageRoundMediatR(e)).GetAwaiter().GetResult();
                    break;
            }
        }

        void Table_ParticipantMessage(object sender, MessageParticipant e)
        {
            switch (e.Action2Participant)
            {
                case ParticipantAction.Added:
                    mediatorTable.PublishAsync(new MessageParticipantMediatR(e)).GetAwaiter().GetResult();
                    receiveMessageParticipants.Add(new ReceiveMessageParticipant(e.ParticipantName));
                    break;
               case ParticipantAction.Booted:
                    mediatorTable.PublishAsync(new MessageParticipantMediatR(e)).GetAwaiter().GetResult();
                    receiveMessageParticipants.RemoveAll(it=>it.NameParticipant==e.ParticipantName);
                    break;
                default:
                    throw  new ArgumentException("message type participant:" +e.Action2Participant);
            }
            
        }

        //public async Task<ReceiveMessageParticipant> AddParticipant(string name)
        //{
        //    if (!TableData.Table.CanAddParticipant)
        //        return null;
        //    await mediatorTable.PublishAsync(new MessageParticipantMediatR()
        //    {
        //        Action2Participant = ParticipantAction.Added,
        //        ParticipantName = name
        //    });
        //    var receive = new ReceiveMessageParticipant(name);
        //    receiveMessageParticipants.Add(receive);
            
        //}


        IEnumerable<object> MultiInstanceFactory(Type serviceType)
        {
            return receiveMessageParticipants;
        }
    }
}
