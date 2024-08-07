using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///NPC dialogue
/// <summary>

public class dialogSystem : MonoBehaviour
{
    [Header("UI���")]
    public Text textLabel;
    public Image faceImage;

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public int index;
    public float textSpeed=0.1f;
    private bool textFinshed;

    [Header("ͷ��")]
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
        //����R��ͬʱ�ж��Ƿ��Ѿ������Ի��ܳ���
        if(Input.GetKeyDown(KeyCode.R)&&index==textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        //����R����Ҫ�ж���һ�仰�Ƿ��Ѿ��������
        if(Input.GetKeyDown(KeyCode.R)&&textFinshed)
        {
            //textLabel.text = textList[index];
            //index++;
            StartCoroutine(SetTextUI());
        }
    }

    //��ȡ����TEXT�ı�����������
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        //�ı������и�
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
            //�ж������������������A�������е�������ռ�õ��������ô������Ӧͷ��
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
            //ÿһ�������ٽ�ÿһ���ְ���ʱ���ų���
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        textFinshed = true;
        index++;
    }
}
