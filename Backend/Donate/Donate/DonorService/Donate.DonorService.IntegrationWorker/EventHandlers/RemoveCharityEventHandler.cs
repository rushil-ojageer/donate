using System.Linq;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.QueueListener;
using Donate.Shared.QueueListener.EventHandler;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.IntegrationWorker.EventHandlers
{
    public class RemoveCharityEventHandler : BaseEventHandler<CharityEvent>
    {
        private readonly DonorContext _donorContext;

        public RemoveCharityEventHandler(DonorContext donorContext)
        {
            _donorContext = donorContext;
        }

        public override string GetService()
        {
            return ServiceNames.CharityService.ToString();
        }

        public override string GetEvent()
        {
            return EventNames.RemoveCharity.ToString();
        }

        protected override async Task ProcessEvent(CharityEvent @event)
        {
            var existingCharity = _donorContext
                .Charities
                .SingleOrDefault(x => x.Identifier == @event.CharityIdentifier);

            if (existingCharity == null)
                return;

            existingCharity.IsDeleted = true;
            await _donorContext.SaveChangesAsync();
        }
    }
}