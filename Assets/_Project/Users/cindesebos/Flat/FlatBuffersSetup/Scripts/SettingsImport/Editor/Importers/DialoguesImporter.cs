using FlatBuffersSetup.Scripts.SettingsImport.Editor;
using FlatBuffersSetup.Gameplay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor.Importers
{
    public class DialoguesImporter : ProjectImporter, IImporter
    {
        protected override string SettingsFileName => "DialoguesSettings.bytes";

        private DialogueSettingsT _currentSettings;

        public DialoguesImporter() : base(ImportConstants.MAIN_CONFIG_SPREADSHEET_ID, "Dialogues") { }

        public async Task DownloadAndParse()
        {
            LocalSettings = new GameSettingsT
            {
                Dialogues = new List<DialogueSettingsT>()
            };

            await DownloadAndParseSheet();
        }

        public void AddToSettings(GameSettingsT gameSettingsT)
        {
            gameSettingsT.Dialogues = LocalSettings.Dialogues;
        }

        protected override void ParseCell(string header, string cellData)
        {
            if (header == "TypeId")
            {
                _currentSettings = new DialogueSettingsT
                {
                    TypeId = cellData
                };

                LocalSettings.Dialogues.Add(_currentSettings);

                return;
            }

            if (header == "Author")
            {
                if (Enum.TryParse(cellData, ignoreCase: true, out AuthorType author)) _currentSettings.AuthorType = author;
            }

            if (header == "MessageLid")
            {
                if (_currentSettings != null) _currentSettings.MessageLid = cellData;

                return;
            }
        }
    }
}