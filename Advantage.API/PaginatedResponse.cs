using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API
{
    public class PaginatedResponse<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PaginatedResponse(IEnumerable<T> data, int index, int length) 
        {
            // Skip x results, take length of data
            Data = data.Skip((index - 1) * length).Take(length).ToList();
            Total = data.Count();
        }
    }
}
