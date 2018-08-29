using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Application.Dto
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Messages = new List<string>();
        }

        public bool Result { get; set; }

        public List<string> Messages { get; set; }
    }
}
