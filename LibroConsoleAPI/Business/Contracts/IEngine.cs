using LibroConsoleAPI.DataAccess;
using LibroConsoleAPI.DataAccess.Contracts;

namespace LibroConsoleAPI.Business.Contracts
{
    public interface IEngine
    {
        Task Run(IBookManager bookManager);
    }
}
