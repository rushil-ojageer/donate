using System;

namespace Donate.Shared.API.Request
{
    public class RequestContext : IRequestContext
    {
        private Guid _requestId;
        private Guid? _parentRequestId;
        private string _user;

        public Guid RequestId => _requestId;

        public Guid? ParentRequestId => _parentRequestId;

        public string User => _user;

        public bool IsAsync { get; set; }


        public void AssignRequestIdentifiers(Guid? requestId, Guid? parentRequestId)
        {
            _requestId = requestId ?? Guid.NewGuid();
            _parentRequestId = parentRequestId;
        }

        public void AssignRequestIdentityInformation(string user)
        {
            _user = user;
        }

        public void Copy(IRequestContext source)
        {
            IsAsync = source.IsAsync;
            AssignRequestIdentifiers(source.RequestId, source.ParentRequestId);
        }
    }
}