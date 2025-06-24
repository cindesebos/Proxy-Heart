using UnityEngine;

namespace Scripts.Gameplay.Canvases.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue Data", menuName = "Datas/New Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [field: SerializeField] public string[] InstanceMessageTypeIds { get; private set; }
        [field: SerializeField] public string[] CosplayMessageTypeIds { get; private set; }
        [field: SerializeField] public string[] MatinsMessageTypeIds { get; private set; }
        [field: SerializeField] public string[] FuryMessageTypeIds { get; private set; }
    }
}
