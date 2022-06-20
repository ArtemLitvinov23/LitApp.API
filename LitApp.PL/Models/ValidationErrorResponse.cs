using System.Collections.Generic;

namespace LitChat.API.Models
{
    public class ValidationErrorResponse
    {
        public List<ValidationError> ErrorResponse { get; set; } = new List<ValidationError>();
    }
}
