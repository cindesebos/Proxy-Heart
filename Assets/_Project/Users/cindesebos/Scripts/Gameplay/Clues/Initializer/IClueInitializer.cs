using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;

namespace Scripts.Gameplay.Clues.Initializer
{
    public interface IClueInitializer
    {
        UniTask<ItemSettingsT> InitializeClueById(string id);
    }
}
