using UnityEngine;

namespace Scripts.Settings.Global
{
    [CreateAssetMenu(fileName = "Global Project Settings", menuName = "Scriptable Objects/Global Project Settings")]
    public class ProjectSettings : ScriptableObject
    {
        [field:SerializeField] public DialogueSettings DialoguesSettings { get; private set; }
    }
}
