using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.Services.Loader.Assets
{
    public interface ILocalAssetLoader
    {
        UniTask<T> Load<T>(string assetId, Transform parent = null) where T : UnityEngine.Object;

        UniTask<Disposable<T>> LoadDisposable<T>(string assetId, Transform parent = null) where T : UnityEngine.Object;
    }
}