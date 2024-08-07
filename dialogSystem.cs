using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///NPC dialogue
/// <summary>

public class dialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed=0.1f;
    private bool textFinshed;

    [Header("头像")]
    public Sprite facezeroone;
    public Sprite facezerotwo;

    List<string> textList = new List<string>();
    void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        //按下R键同时判断是否已经超过对话总长度
        if(Input.GetKeyDown(KeyCode.R)&&index==textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        //按下R键需要判断上一句话是否已经播放完成
        if(Input.GetKeyDown(KeyCode.R)&&textFinshed)
        {
            //textLabel.text = textList[index];
            //index++;
            StartCoroutine(SetTextUI());
        }
    }

    //获取我们TEXT文本中所有内容
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        //文本按行切割
        var LineDate= file.text.Split('\n');
        foreach(var Line in LineDate)
        {
            textList.Add(Line);
        }
    }


    IEnumerator SetTextUI()
    {
        textFinshed = false;
        textLabel.text = "";

        switch(textList[index])
        {
            //判断整行如果整行内容是A（避免有单独出现占用的情况）那么播放相应头像
            case "A":
                faceImage.sprite = facezeroone;
                index++;
                break;
            case "B":
                faceImage.sprite = facezerotwo;
                index++;
                break;
        }

        for (int i = 0; i < textList[index].Length;i++)
        {  
            //每一行内容再将每一个字按延时播放出来
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        textFinshed = true;
        index++;
    }
}
