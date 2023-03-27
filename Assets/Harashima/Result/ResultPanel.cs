using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class ResultPanel : SceneControllerBase
{
    [SerializeField]
    Text _scoreText;

    [SerializeField]
    List<ResultCell> _resultCells = new List<ResultCell>();

    [SerializeField]
    Button _touchArea;

    CanvasGroup _canvasGroup;

    protected override void OnStart()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts= false;
        _touchArea.onClick.AddListener(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await SceneChangeAsync();
        });
    }

    public void SetupResultPanel(string name,int score)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        SetScore(score,name);
        SetRankingCells(); 
    }

    private void SetRankingCells()
    {
        if (ApplicationManager.Instanse.RankingUsers.Length > _resultCells.Count)
        {
            return;
        }
        for (int i = 0; i < ApplicationManager.Instanse.RankingUsers.Length; i++)
        {
            _resultCells[i].SetCell(ApplicationManager.Instanse.RankingUsers[i]);
        }
    }
    private void SetScore(int score,string name)
    {
        // Todo アニメーションする
        _scoreText.text = score.ToString();
        ApplicationManager.Instanse.AddRankingList(name,score);
    }
}
