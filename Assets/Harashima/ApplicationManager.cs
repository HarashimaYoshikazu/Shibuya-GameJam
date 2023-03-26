using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Rank
{
    First,
    Second
}
public class ApplicationManager : MonoBehaviour
{
    Dictionary<Rank, string> _rankingDictionary = new Dictionary<Rank, string>();

    public string[] PlayerNames => _rankingDictionary.Values.ToArray();

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void AddRankingDictionary(Rank rank,string playerName)
    {
        _rankingDictionary.Add(rank,playerName);
    }
}
