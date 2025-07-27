using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class RankInfo
{
    public int id;
    public string name;
}


[CreateAssetMenu(fileName = "RankTable", menuName = "Scripts/Data/Player", order = 1)]
public class RankTable : ScriptableObject
{
    public List<RankInfo> ranks;
    private Dictionary<int, RankInfo> _map;

    private void OnEnable()
    {
        _map = ranks.ToDictionary(r => r.id);
    }

    public RankInfo Get(int id) => _map.TryGetValue(id, out var info) ? info : null;
}
