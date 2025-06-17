using FlatBuffersSetup.Scripts.SettingsImport.Editor;
using FlatBuffersSetup.Gameplay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor.Importers
{
    public class ClueGroupsImporter : ProjectImporter, IImporter
    {
        protected override string SettingsFileName => "ClueGroupsSettings.bytes";

        private ClueGroupSettingsT _currentSettings;

        public ClueGroupsImporter() : base(ImportConstants.MAIN_CONFIG_SPREADSHEET_ID, "ClueGroups") { }

        public async Task DownloadAndParse()
        {
            LocalSettings = new GameSettingsT
            {
                ClueGroups = new List<ClueGroupSettingsT>()
            };

            await DownloadAndParseSheet();
        }

        public void AddToSettings(GameSettingsT gameSettingsT)
        {
            gameSettingsT.ClueGroups = LocalSettings.ClueGroups;
        }

        protected override void ParseCell(string header, string cellData)
        {
            if (header == "TypeId")
            {
                _currentSettings = new ClueGroupSettingsT
                {
                    TypeId = cellData
                };

                LocalSettings.ClueGroups.Add(_currentSettings);

                return;
            }

            if (header == "TitleLid")
            {
                if (_currentSettings != null) _currentSettings.TitleLid = cellData;

                return;
            }

            if (header == "CorrectIds")
            {
                if (_currentSettings != null)
                {
                    _currentSettings.CorrectIds = new List<string>(cellData.Split(','));

                    UnityEngine.Debug.Log("_currentSettings.CorrectId: "  +  _currentSettings.CorrectIds);
                }

                return;
            }
        }
    }
}