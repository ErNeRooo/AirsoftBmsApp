using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking
{
    public abstract record HttpResult<D>();

    public record Success<D>(D data) : HttpResult<D>;
    public record Failure<D>(string errorMessage) : HttpResult<D>;
}
