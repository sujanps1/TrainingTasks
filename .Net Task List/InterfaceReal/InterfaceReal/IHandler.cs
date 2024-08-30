using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InterfaceReal
{
    public interface IHandler
    {
        Task<object> HandleRequestAsync(HttpContext context);

    }
}
