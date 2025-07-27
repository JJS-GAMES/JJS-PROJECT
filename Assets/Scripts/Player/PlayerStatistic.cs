using UnityEngine;

public class PlayerStatistic : MonoBehaviour
{
    [SerializeField] private string _nickname = "NickName";
    private int _badgeNumber;
    private int _rankId;

    public int BadgeNumber => _badgeNumber;
    public string Nickname => _nickname;
    public int RankId => _rankId;

    private void Awake()
    {
        if (DatabaseManager.Instance == null)
        {
            Debug.LogError("DatabaseManager не найден!");
            return;
        }

        var pd = DatabaseManager.Instance.GetOrCreatePlayer(_nickname);
        _badgeNumber = pd.BadgeNumber;
        _rankId = pd.RankId;
        _nickname = pd.Nickname;
    }

    private void Start()
    {
        UIManager.Instance.UpdateStatisticUI(this);
    }
}
