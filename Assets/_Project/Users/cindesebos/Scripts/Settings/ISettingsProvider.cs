using Cysharp.Threading.Tasks;
using FlatBuffersSetup;
using Scripts.Settings.Global;

namespace Scripts.Settings
{
    public interface ISettingsProvider
    {
        GameSettings GameSettings { get; }
        ProjectSettings ProjectSettings { get; }

        UniTask LoadAllSettingsAsync();
    }
}
