using FlatBuffersSetup.Scripts.SettingsImport.Editor.Importers;
using UnityEditor;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor
{
    public static class ImportingMenu
    {
        [MenuItem("Flat/Import Settings/Import All Configs")]
        private static void ImportAllConfigs() => SettingsImportUtils.ImportAllConfigs();
        
        [MenuItem("Flat/Import Settings/Import Items Settings")]
        private static void ImportItemsSettings()
        {
            var importer = new ItemsImporter();

            SettingsImportUtils.ImportConfig(importer);
        }
    }
}