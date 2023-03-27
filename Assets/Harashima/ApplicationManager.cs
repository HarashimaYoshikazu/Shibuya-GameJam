using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    List<RankingUser> _rankingUsers = new List<RankingUser>();

    public RankingUser[] RankingUsers => _rankingUsers.ToArray();

    private static ApplicationManager _instanse;
    public static ApplicationManager Instanse => _instanse;

    private void Awake()
    {
        if(Instanse)
        {
            Destroy(this.gameObject);
        }
        _instanse = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void AddRankingList(string playerName, int score)
    {

        var newuser = new RankingUser()
        {
            PlayerName = playerName,
            Score = score
        };
        _rankingUsers.Add(newuser);
        _rankingUsers.OrderBy(user => user.Score);
        if (_rankingUsers.Count>5)
        {
            _rankingUsers.RemoveAt(_rankingUsers.Count - 1);
        }    
    }
}

public class RankingUser
{
    public string PlayerName;
    public int Score;
}
