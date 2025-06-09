using System;
using Cysharp.Threading.Tasks;

namespace Scripts.Services.Loader
{
    public interface ISceneLoader
    {
        event Action OnLoadingStart;

        event Action OnLoadingEnd;

        void LoadScene(Scene scene);

        UniTask LoadSceneAsync(Scene scene);
    }
}