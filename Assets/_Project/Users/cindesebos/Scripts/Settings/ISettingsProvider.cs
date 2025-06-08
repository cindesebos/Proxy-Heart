using Cysharp.Threading.Tasks;
using FlatBuffersSetup;

namespace Scripts.Settings
{
    public interface ISettingsProvider
    {
        GameSettings GameSettings { get; }

        UniTask LoadAllSettingsAsync();
    }
}
