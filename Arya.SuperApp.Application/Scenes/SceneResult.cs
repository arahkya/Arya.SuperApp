namespace Arya.SuperApp.Application.Scenes;

public class SceneResult<TResult> 
{
    public TResult Result { get; internal set; } = default!;
}