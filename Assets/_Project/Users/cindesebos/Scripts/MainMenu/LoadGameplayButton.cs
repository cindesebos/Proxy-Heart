using Scripts.Services.Loader.Assets;
using Scripts.Services.Loader.Scenes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.MainMenu
{
    public class LoadGameplayButton : MonoBehaviour
    {
        [SerializeField] private Scene _sceneToLoad;
        [SerializeField] private Button _button;

        private void OnValidate() => _button ??= GetComponent<Button>();

        [Inject]
        private async void Construct(ISceneLoader sceneLoader, ILocalAssetLoader localAssetLoader)
        {
            _button.onClick.AddListener(async delegate
            {
                using (var loadingCanvas = await localAssetLoader.LoadDisposable<GameObject>(AssetsConstants.LoadingCanvas))
                {
                    await sceneLoader.LoadSceneAsync(_sceneToLoad);
                }
            });

        }
    }
}
