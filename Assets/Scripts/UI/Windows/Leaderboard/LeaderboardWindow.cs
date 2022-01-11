using System.Collections.Generic;
using Services.Database;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Leaderboard
{
  public class LeaderboardWindow : BaseWindow
  {
    [SerializeField] private Button backButton;
    [SerializeField] private RectTransform content;
    [SerializeField] private PlayerLeaderboardField fieldPrefab;
    
    private IDatabaseService databaseService;

    public void Construct(IDatabaseService databaseService)
    {
      this.databaseService = databaseService;
      SetLeaderboard();
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      backButton.onClick.AddListener(Close);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      backButton.onClick.RemoveListener(Close);
    }

    public override void Close() => 
      Destroy(gameObject);

    private async void SetLeaderboard()
    {
      IEnumerable<LeaderboardPlayer> players;
      if (databaseService.IsNeedToUpdateLeaderboard())
        players = await databaseService.UpdateTopPlayers();
      else
        players = databaseService.Leaderboard;
      
      CreateFields(players);
    }

    private void CreateFields(IEnumerable<LeaderboardPlayer> leaderboardPlayers)
    {
      PlayerLeaderboardField field;
      foreach (LeaderboardPlayer player in leaderboardPlayers)
      {
        field = Instantiate(fieldPrefab, content);
        field.SetPlayer(player.Nickname, player.Score);
      }
    }
    
    
  }
}
