using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Donate.Shared.API.Models
{
    public interface IModel<T> where T : DbContext
    {
        Task<ValidationResults> Validate(T context);
    }
}