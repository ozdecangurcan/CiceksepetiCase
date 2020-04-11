using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Core.Filters.Model
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
