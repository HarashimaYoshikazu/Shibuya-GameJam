using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChatGPTController : MonoBehaviour
{
    [SerializeField]
    Button _button;

    [SerializeField]
    InputField _inputField;
    void Start()
    {
        _button.onClick.AddListener(async () =>
        {
            if (_inputField == null || _inputField.text == "" || _inputField.text == null)
            {
                return;
            }
            var chatGPTConnection = new ChatGPTConnection("今から名前を提示するのでそれに対して以下の条件に沿った反応を20文字以内で出力してください。・語尾に「わん」を付けてください。・渋谷の町に関連する単語を入れてください。","");

            var responseModel = await chatGPTConnection.RequestAsync(_inputField.text);
        });
    }
}
