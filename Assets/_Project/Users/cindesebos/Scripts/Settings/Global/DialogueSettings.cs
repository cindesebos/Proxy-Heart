using System;
using UnityEngine;

namespace Scripts.Settings.Global
{
    [Serializable]
    public class DialogueSettings
    {
        [field: SerializeField] public string[] InitialMessagesTypeIds { get; set; }
    }
}
