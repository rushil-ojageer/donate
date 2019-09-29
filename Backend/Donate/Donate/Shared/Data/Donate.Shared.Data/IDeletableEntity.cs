using System;

namespace Donate.Shared.Data
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }

    public interface IAuditableEntity
    {
        DateTime UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}