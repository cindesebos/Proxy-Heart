using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;

namespace Scripts.Gameplay.Clues.Initializer
{
    public interface IClueInitializer
    {
        UniTask<ClueTextSettingsT> InitializeClueTextById(string id);
        UniTask<ClueObjectSettingsT> InitializeClueObjectById(string id);
        UniTask<ClueGroupSettingsT> InitializeGroupVariantById(string typeId);
        UniTask<string> InitializeClueById(string id);
    }
}
