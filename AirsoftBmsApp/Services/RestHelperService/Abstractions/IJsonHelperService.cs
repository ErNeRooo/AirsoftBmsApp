using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.RestHelperService.Abstractions
{
    public interface IJsonHelperService
    {
        StringContent GetStringContent(object dto);
        Task<T> DeserializeFromResponseAsync<T>(HttpResponseMessage response)
            where T : class;
    }
}
