using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Scripts.Services.Loader
{
    public class SceneLoader : ISceneLoader
    {
        public event Action OnLoadingStart;

        public event Action OnLoadingEnd;

        public void LoadScene(Scene scene) => LoadSceneAsync(scene).Forget();

        public async UniTask LoadSceneAsync(Scene scene)
        {
            try
            {
                OnLoadingStart?.Invoke();

                string sceneName = scene.ToString();

                AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

                while (!loadSceneOperation.isDone) await UniTask.Yield();

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            }
            finally 
            {
                OnLoadingEnd?.Invoke();
            }
        }
    }
}