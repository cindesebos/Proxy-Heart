using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;

namespace Scripts.Gameplay.Clues.Initializer
{
    public interface IClueInitializer
    {
        UniTask<ClueObjectSettingsT> InitializeClueObjectById(string id);
        UniTask<ClueGroupSettingsT> InitializeGroupVariantById(string typeId);
        UniTask<string> InitializeClueById(string id);
        UniTask<GoogleSettingsT> InitializeGoogleById(string id);
    }
}
