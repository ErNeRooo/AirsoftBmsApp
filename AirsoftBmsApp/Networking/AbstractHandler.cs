using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking
{
    public class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }
        public virtual async Task<HttpResult> Handle(object request)
        {
            if (_nextHandler != null)
            {
                return await _nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
