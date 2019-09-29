using System;
using System.Linq;
using System.Threading.Tasks;
using Donate.DonorService.Data;
using Donate.DonorService.Data.Entities;
using Donate.Shared.Eventing;
using Donate.Shared.Eventing.IntegrationEvents;
using Donate.Shared.QueueListener;
using Donate.Shared.QueueListener.EventHandler;
using Microsoft.EntityFrameworkCore;

namespace Donate.DonorService.IntegrationWorker.EventHandlers
{
    public class AddCharityEventHandler : BaseEventHandler<CharityEvent>
    {
        private readonly DonorContext _donorContext;

        public AddCharityEventHandler(DonorContext donorContext)
        {
            _donorContext = donorContext;
        }

        public override string GetService()
        {
            return ServiceNames.CharityService.ToString();
        }

        public override string GetEvent()
        {
            return EventNames.AddCharity.ToString();
        }

        protected override async Task ProcessEvent(CharityEvent @event)
        {
            var exists = _donorContext
                .Charities
                .Any(x => x.Identifier == @event.CharityIdentifier);

            if (exists)
                return;

            var charity = new Charity
            {
                Identifier = @event.CharityIdentifier,
                CharityName = @event.CharityName,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "IntegrationEvent"
            };

            _donorContext.Charities.Add(charity);

            await _donorContext.SaveChangesAsync();
        }
    }
}
