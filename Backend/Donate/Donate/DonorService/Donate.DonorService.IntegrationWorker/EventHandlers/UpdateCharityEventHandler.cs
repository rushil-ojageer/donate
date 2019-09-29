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
    public class UpdateCharityEventHandler : BaseEventHandler<CharityEvent>
    {
        private readonly DonorContext _donorContext;

        public UpdateCharityEventHandler(DonorContext donorContext)
        {
            _donorContext = donorContext;
        }
        public override string GetService()
        {
            return ServiceNames.CharityService.ToString();
        }

        public override string GetEvent()
        {
            return EventNames.UpdateCharity.ToString();
        }

        protected override async Task ProcessEvent(CharityEvent @event)
        {
            var exists = _donorContext
                .Charities
                .SingleOrDefault(x => x.Identifier == @event.CharityIdentifier);

            if (exists == null)
            {
                exists = new Charity()
                {
                    Identifier = @event.CharityIdentifier
                };
                _donorContext.Charities.Add(exists);
            }

            exists.CharityName = @event.CharityName;
            await _donorContext.SaveChangesAsync();
        }
    }
}