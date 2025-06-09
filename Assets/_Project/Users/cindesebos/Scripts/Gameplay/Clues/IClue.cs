using Cysharp.Threading.Tasks;
using FlatBuffersSetup.Gameplay;
using UnityEngine;

namespace Scripts.Gameplay.Clues
{
    public interface IClue
    {
        string TypeId { get; }
        string TitleLid { get; }

        UniTask Initialize(ItemSettingsT settings);
    }
}
