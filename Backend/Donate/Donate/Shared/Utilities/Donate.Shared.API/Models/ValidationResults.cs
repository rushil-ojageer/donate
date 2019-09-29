using System;
using System.Collections.Generic;
using System.Linq;

namespace Donate.Shared.API.Models
{
    public class ValidationResults
    {
        public ValidationResults()
        {
            ValidationErrors = new List<ModelError>();
        }

        public bool IsValid => !ValidationErrors.Any();

        public IList<ModelError> ValidationErrors { get; }

        public void AddError(string field, string error)
        {
            ValidationErrors.Add(new ModelError());
        }

        public void AddError(string error)
        {
            ValidationErrors.Add(new ModelError());
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, ValidationErrors.Select(x => x.ErrorMessage));
        }
    }
}