using Arya.SuperApp.Application.Scenes;

namespace Arya.SuperApp.Application.Interfaces.Scene;

public interface ISceneHandler<in TRequest, TResult>
{
    Task<SceneResult<TResult>> HandleAsync(TRequest request, Func<Exception, TResult> onError);
}