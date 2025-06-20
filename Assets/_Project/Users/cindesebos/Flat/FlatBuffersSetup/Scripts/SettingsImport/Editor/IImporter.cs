using System.Threading.Tasks;

namespace FlatBuffersSetup.Scripts.SettingsImport.Editor
{
    public interface IImporter
    {
        string SheetName { get; }
        
        Task DownloadAndParse();
        
        void AddToSettings(GameSettingsT gameSettingsT);

        void SaveToFile();
        
        void LoadFromFile();
    }
}