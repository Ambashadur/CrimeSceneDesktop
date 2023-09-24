using System.IO;
using System.Threading.Tasks;
using CrimeSceneDesktop.Contracts;
using CrimeSceneDesktop.Contracts.Scenes;

namespace CrimeSceneDesktop.Common.Services;

public interface ISceneService
{
    Task<Scene> CreateSceneAsync(
        string name,
        Stream filestream,
        string contentType,
        string filename);

    Task<PageResult<Scene>> GetScenePageAsync(GetPageContext context);
}
