using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCF.Models.Responses.ResponseDto
{
    public class GenericResponse<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Error { get; set; }
        public T Result { get; set; }

        public GenericResponse()
        {
            IsSuccess = true;
            Error = new List<string>();
        }
        public GenericResponse(T Result)
        {
            IsSuccess = true;
            Error = new List<string>();
            this.Result = Result;
        }

        public void AddError(string error)
        {
            Error.Add(error);
            IsSuccess = false;
        }
    }
}
