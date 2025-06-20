using FlatBuffersSetup.Scripts.SettingsImport.Editor;
using FlatBuffersSetup.Gameplay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor.Importers
{
    public class ClueObjectsImporter : ProjectImporter, IImporter
    {
        protected override string SettingsFileName => "ClueObjectsSettings.bytes";

        private ClueObjectSettingsT _currentSettings;

        public ClueObjectsImporter() : base(ImportConstants.MAIN_CONFIG_SPREADSHEET_ID, "ClueObjects") { }

        public async Task DownloadAndParse()
        {
            LocalSettings = new GameSettingsT
            {
                ClueObjects = new List<ClueObjectSettingsT>()
            };

            await DownloadAndParseSheet();
        }

        public void AddToSettings(GameSettingsT gameSettingsT)
        {
            gameSettingsT.ClueObjects = LocalSettings.ClueObjects;
        }

        protected override void ParseCell(string header, string cellData)
        {
            if (header == "TypeId")
            {
                UnityEngine.Debug.Log($"header: {header}  and cellData: {cellData}");

                _currentSettings = new ClueObjectSettingsT
                {
                    TypeId = cellData
                };

                LocalSettings.ClueObjects.Add(_currentSettings);
                return;
            }

            if (header == "TitleLID")
            {
                UnityEngine.Debug.Log($"header: {header}  and cellData: {cellData}");

                if (_currentSettings != null) _currentSettings.TitleLid = cellData;

                return;
            }
        }
    }
}