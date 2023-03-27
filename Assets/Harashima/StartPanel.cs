using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

public class StartPanel : MonoBehaviour
{
    [SerializeField]
    InputField _nameInputField;
    [SerializeField]
    Text _dogText;
    [SerializeField]
    Button _button;
    [SerializeField]
    CanvasGroup _loadingPanel;

    CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha= 1.0f;
        _loadingPanel.alpha = 0f;
        _loadingPanel.blocksRaycasts= false;
        Setup();
    }
    public void Setup()
    {
        _button.onClick.AddListener(async () =>
        {
            _canvasGroup.blocksRaycasts = false;         
            if (_nameInputField.text == "" || _nameInputField.text == null)
            {
                return;
            }
            var chatGPTConnection = new ChatGPTConnection("今から名前を提示するのでそれに対して以下の条件に沿った反応を20文字以内で出力してください。・語尾に「わん」を付けてください。・渋谷の町に関連する単語を入れてください。・面白い単語を入れてください。・提示した名前に関して言及してください。");
            _loadingPanel.alpha= 1.0f;
            _loadingPanel.blocksRaycasts = true;

            var responseModel = await chatGPTConnection.RequestAsync(_nameInputField.text);
            _dogText.text = responseModel.choices[0].message.content;

            _loadingPanel.alpha = 0f;
            _loadingPanel.blocksRaycasts = false;
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            await DOVirtual.Float(1f, 0f, 1f, value =>
            {
                _canvasGroup.alpha = value;
            }).ToUniTask();
            GameManager.Instance.IsPause = true;
        });
    }
}
