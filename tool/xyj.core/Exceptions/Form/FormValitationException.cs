using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace xyj.core.Exceptions.Form
{
    public class FormValitationException : System.Exception
    {
        public List<DbValidationError> ValidationErrors;
       
        public FormValitationException(List<DbValidationError> validationErrors)
        {
            this.ValidationErrors = validationErrors;
        }
    }
}
