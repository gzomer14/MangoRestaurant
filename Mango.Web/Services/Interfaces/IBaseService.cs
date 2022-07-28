using Mango.Web.Models;
using Mango.Web.Models.DTO;

namespace Mango.Web.Services.Interfaces
{
    public interface IBaseService : IDisposable
    {
        ResponseDTO responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
