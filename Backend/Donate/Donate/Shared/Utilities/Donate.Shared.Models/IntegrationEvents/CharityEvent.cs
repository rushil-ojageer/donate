using System;

namespace Donate.Shared.Eventing.IntegrationEvents
{
    public class CharityEvent
    {
        public Guid CharityIdentifier { get; set; }
        public string CharityName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
