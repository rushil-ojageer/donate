using System;

namespace Donate.Shared.API.Request
{
    public interface IRequestContext
    {
        Guid RequestId { get; }
        Guid? ParentRequestId { get; }
        string User { get; }
        bool IsAsync { get; set; }
        void AssignRequestIdentifiers(Guid? requestId, Guid? parentRequestId);
        void AssignRequestIdentityInformation(string user);
        void Copy(IRequestContext source);
    }
}
