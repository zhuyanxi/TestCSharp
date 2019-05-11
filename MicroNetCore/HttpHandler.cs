using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroNetCore
{
    public delegate Task RequestDelegate(HttpContext context);
}
