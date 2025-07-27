using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI _statisticText;

    [Header("Ranks")]
    [SerializeField] private RankTable _rankTable;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Обновить HUD по данным игрока.
    /// </summary>
    public void UpdateStatisticUI(PlayerStatistic ps)
    {
        var info = _rankTable.Get(ps.RankId) ?? new RankInfo { id = 0, name = "Без звания" };

        _statisticText.text = $"{ps.BadgeNumber:D4} | \"{ps.Nickname}\" | {info.name}";
    }
}
