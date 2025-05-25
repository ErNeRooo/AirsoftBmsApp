using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        Task<HttpResult> Handle(object request);
    }
}
