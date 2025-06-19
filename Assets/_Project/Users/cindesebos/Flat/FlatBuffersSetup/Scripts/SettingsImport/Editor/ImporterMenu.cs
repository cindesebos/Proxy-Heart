using FlatBuffersSetup.Scripts.SettingsImport.Editor.Importers;
using UnityEditor;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor
{
    public static class ImportingMenu
    {
        [MenuItem("Flat/Import Settings/Import All Configs")]
        private static void ImportAllConfigs() => SettingsImportUtils.ImportAllConfigs();

        [MenuItem("Flat/Import Settings/Import Clue Texts Settings")]
        private static void ImportClueTextsSettings()
        {
            var importer = new ClueTextsImporter();

            SettingsImportUtils.ImportConfig(importer);
        }

        [MenuItem("Flat/Import Settings/Import Clue Objects Settings")]
        private static void ImportClueObjectsSettings()
        {
            var importer = new ClueObjectsImporter();

            SettingsImportUtils.ImportConfig(importer);
        }

        [MenuItem("Flat/Import Settings/Import Clue Groups Settings")]
        private static void ImportClueGroupsSettings()
        {
            var importer = new ClueGroupsImporter();

            SettingsImportUtils.ImportConfig(importer);
        }
        
        [MenuItem("Flat/Import Settings/Import Dialogues Settings")]
        private static void ImportDialoguesSettings()
        {
            var importer = new DialoguesImporter();

           SettingsImportUtils.ImportConfig(importer);
        }
    }
}