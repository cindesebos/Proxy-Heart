using FlatBuffersSetup.Scripts.SettingsImport.Editor;
using FlatBuffersSetup.Gameplay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor.Importers
{
    public class GooglesImporter : ProjectImporter, IImporter
    {
        protected override string SettingsFileName => "GooglesSettings.bytes";

        private GoogleSettingsT _currentSettings;

        public GooglesImporter() : base(ImportConstants.MAIN_CONFIG_SPREADSHEET_ID, "Googles") { }

        public async Task DownloadAndParse()
        {
            LocalSettings = new GameSettingsT
            {
                Googles = new List<GoogleSettingsT>()
            };

            await DownloadAndParseSheet();
        }

        public void AddToSettings(GameSettingsT gameSettingsT)
        {
            gameSettingsT.Googles = LocalSettings.Googles;
        }

        protected override void ParseCell(string header, string cellData)
        {
            if (header == "VariantId")
            {
                _currentSettings = new GoogleSettingsT
                {
                    VariantId = cellData
                };

                LocalSettings.Googles.Add(_currentSettings);
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