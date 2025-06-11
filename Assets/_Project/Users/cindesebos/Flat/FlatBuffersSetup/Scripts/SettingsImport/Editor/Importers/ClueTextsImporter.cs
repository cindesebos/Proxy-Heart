using FlatBuffersSetup.Scripts.SettingsImport.Editor;
using FlatBuffersSetup.Gameplay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor.Importers
{
    public class ClueTextsImporter : ProjectImporter, IImporter
    {
        protected override string SettingsFileName => "ClueTextsSettings.bytes";

        private ClueTextSettingsT _currentSettings;

        public ClueTextsImporter() : base(ImportConstants.MAIN_CONFIG_SPREADSHEET_ID, "ClueTexts") { }

        public async Task DownloadAndParse()
        {
            LocalSettings = new GameSettingsT
            {
                ClueTexts = new List<ClueTextSettingsT>()
            };

            await DownloadAndParseSheet();
        }

        public void AddToSettings(GameSettingsT gameSettingsT)
        {
            gameSettingsT.ClueTexts = LocalSettings.ClueTexts;
        }

        protected override void ParseCell(string header, string cellData)
        {
            if (header == "TypeId")
            {
                _currentSettings = new ClueTextSettingsT
                {
                    TypeId = cellData
                };

                LocalSettings.ClueTexts.Add(_currentSettings);
                return;
            }

            if (header == "MessageLid")
            {
                if (_currentSettings != null) _currentSettings.MessageLid = cellData;

                return;
            }
        }
    }
}