using Services.Progress;

namespace Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private IPersistentProgressService progressService;
    
    public SaveLoadService(IPersistentProgressService progressService)
    {
      this.progressService = progressService;
    }

    public void SaveProgress()
    {
      
    }

    public void LoadProgress()
    {
    }
  }
}