using System;

namespace Scripts.Services.Loader.Assets
{
    public class Disposable<T> : IDisposable where T : UnityEngine.Object
    {
        public T Value { get; }
        private readonly Action _onDispose;

        public Disposable(T value, Action onDispose)
        {
            Value = value;
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose?.Invoke();
        }
    }
}