using MediatR;

namespace PPMessagingMediatR
{
    public class MediatorTable : Mediator
    {
        public MediatorTable(MultiInstanceFactory mt)
            : base(null, mt)
        {
            

        }
       

        
    }
}