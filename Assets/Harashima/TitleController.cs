using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;

public class TitleController : SceneControllerBase
{
    [SerializeField]
    Button _startButton;

    protected override void OnStart()
    {
        _startButton.onClick.AddListener(async () =>
        {
            await SceneChangeAsync();
        });
    }
}
