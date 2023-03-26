using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestChatGPTController : MonoBehaviour
{
    [SerializeField]
    Button _button;
    void Start()
    {
        _button.onClick.AddListener(async () =>
        {
            var chatGPTConnection = new ChatGPTConnection("dog","今から名前を提示するのでそれに対してポジティブな反応を15文字以内でお願いします。また、語尾に「わん」を付けてください。");
            await chatGPTConnection.RequestAsync("");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
