using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Application.Dto
{
    public class ServiceResponse<T> : ServiceResult
    {
        public T Object { get; set; }
    }
}
