using System;
using Cysharp.Threading.Tasks;

namespace Scripts.Services.Loader.Scenes
{
    public interface ISceneLoader
    {
        event Action OnLoadingStarted;

        event Action OnLoadingEnded;

        void LoadScene(Scene scene);

        UniTask LoadSceneAsync(Scene scene);
    }
}