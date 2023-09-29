using CS.Contracts;
using CS.Contracts.Scenes;

namespace CS.Common.Services;

public interface ISceneService
{
    Task<Scene> CreateSceneAsync(string name, Stream filestream, string contentType, string filename);

    Task<PageResult<Scene>> GetScenePageAsync(GetPageContext context);
}
