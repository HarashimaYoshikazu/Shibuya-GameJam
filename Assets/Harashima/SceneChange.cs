using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

public class SceneChange
{
    public async UniTask SceneChangeAsync(string sceneName,Image fadePanel, CancellationToken token, float endValue = 1f,float duration = 1f)
    {
        await fadePanel.DOFade(endValue,duration).ToUniTask(cancellationToken: token);
        await SceneManager.LoadSceneAsync(sceneName);
    }
}
