using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Donate.Shared.API.Models
{
    public class BaseModel<T> : IModel<T> where T : DbContext
    { 
        protected bool IsValidated;

        public virtual Task<ValidationResults> Validate(T db)
        {
            return Task.FromResult(new ValidationResults());
        }
    }
}