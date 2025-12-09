using System.Collections;
using Arya.SuperApp.Application.Interfaces.Scene;

namespace Arya.SuperApp.Application.Scenes;

public class SceneCollectionResult<TResult>(IEnumerable<TResult> results) : SceneResult<IReadOnlyCollection<TResult>>, IReadOnlyCollection<TResult>, ISceneCollectionResult
{
    public IEnumerator<TResult> GetEnumerator()
    {
        return results.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)results).GetEnumerator();
    }

    public int Count => results.Count();
}