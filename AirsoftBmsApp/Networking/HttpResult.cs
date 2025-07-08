using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking;

public abstract record HttpResult();

public record Success() : HttpResult;
public record Failure(string errorMessage) : HttpResult;
public record Error(string errorMessage) : HttpResult;
