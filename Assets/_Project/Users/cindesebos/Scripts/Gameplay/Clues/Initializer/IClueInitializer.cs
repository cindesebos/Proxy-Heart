using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;

namespace Scripts.Gameplay.Clues.Initializer
{
    public interface IClueInitializer
    {
        UniTask<ClueTextSettingsT> InitializeClueTextById(string id);
        UniTask<ClueObjectSettingsT> InitializeClueObjectById(string id);
        UniTask<ClueGroupSettingsT> InitializeGriupVariantById(string typeId);
        UniTask<string> InitializeClueById(string id);
    }
}
