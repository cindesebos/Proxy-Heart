using FlatBuffers.Editor;
using UnityEditor;

namespace FlatBuffersSetup.Scripts.FlatBuffers.Editor
{
    public static class FlatBuffersMenu
    {
        private const string OutPutDirectory = "Assets/_Project/Users/cindesebos/Flat/FlatBuffersSetup/Scripts/Generated/fbs";
        
        private static readonly string[] _schemaPaths =
        {
            "Assets/_Project/Users/cindesebos/Flat/FlatBuffersSetup/Scripts/Shared/Schemas/GameSettings.fbs",
            "Assets/_Project/Users/cindesebos/Flat/FlatBuffersSetup/Scripts/Shared/Schemas/Gameplay/ClueObjects.fbs",
            "Assets/_Project/Users/cindesebos/Flat/FlatBuffersSetup/Scripts/Shared/Schemas/Gameplay/ClueTexts.fbs",
            "Assets/_Project/Users/cindesebos/Flat/FlatBuffersSetup/Scripts/Shared/Schemas/Gameplay/ClueGroups.fbs",
            "Assets/_Project/Users/cindesebos/Flat/FlatBuffersSetup/Scripts/Shared/Schemas/Gameplay/Dialogues.fbs"
        };

        [MenuItem("Flat/FlatBuffers/Compile FlatBuffers Schemas")]
        public static void CompileFlatBuffersSchemas() => FlatcCompiler.Compile(_schemaPaths, OutPutDirectory);
    }
}