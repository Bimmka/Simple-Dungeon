using TMPro;
using UnityEngine;

namespace UI.Windows.Leaderboard
{
  public class PlayerLeaderboardField : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetPlayer(string nickname, int score)
    {
      nicknameText.text = nickname;
      scoreText.text = score.ToString();
    }
  }
}