namespace Donate.Shared.API.Models
{
    public class ModelError
    {
        private readonly string _field;
        private readonly string _error;
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }

        public ModelError()
        {
            
        }

        public ModelError(string field, string error)
        {
            _field = field;
            _error = error;
        }
    }
}