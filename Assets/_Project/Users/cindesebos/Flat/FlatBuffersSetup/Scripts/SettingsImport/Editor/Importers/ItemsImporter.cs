using FlatBuffersSetup.Scripts.SettingsImport.Editor;
using FlatBuffersSetup.Gameplay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor.Importers
{
    public class ItemsImporter : ProjectImporter, IImporter
    {
        protected override string SettingsFileName => "ItemsSettings.bytes";

        private ItemSettingsT _currentBuildingSettings;

        public ItemsImporter() : base(ImportConstants.MAIN_CONFIG_SPREADSHEET_ID, "Items") { }

        public async Task DownloadAndParse()
        {
            LocalSettings = new GameSettingsT
            {
                Items = new List<ItemSettingsT>()
            };

            await DownloadAndParseSheet();
        }

        public void AddToSettings(GameSettingsT gameSettingsT)
        {
            gameSettingsT.Items = LocalSettings.Items;
        }

        protected override void ParseCell(string header, string cellData)
        {
            if (header == "TypeId")
            {
                _currentBuildingSettings = new ItemSettingsT
                {
                    TypeId = cellData
                };
                LocalSettings.Items.Add(_currentBuildingSettings);
                return;
            }

            if (header == "TitleLID")
            {
                if (_currentBuildingSettings != null)
                {
                    _currentBuildingSettings.TitleLid = cellData;
                }

                return;
            }
        }
    }
}