using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���˵������࣬�̳��Ե���ģʽ���࣬���ڹ������˵������߼�
public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField]
    private MainMenuText title;          // �˵������ı����
    [SerializeField]
    private MainMenuText description;    // �˵������ı����
    [SerializeField]
    private ChapterPanel ChapterPanel;   // �½�ѡ��������

    // ���Է�����
    public MainMenuText Title => title;
    public MainMenuText Description => description;

    // ��ǰѡ����½ں͹ؿ�������-1��ʾδѡ��״̬��
    private int currentChooseChapter = -1;
    private int currentChooseLevel = -1;

    private void Start()
    {
        // ��ʼ����������λ������������߼���
    }

    /// <summary>
    /// ��ʼ���ؿ����ݲ�����UI״̬
    /// 1. Ĭ�Ͻ��������½ڰ�ť
    /// 2. ���ݹؿ�����״̬���¶�Ӧ��ť�Ľ���״̬����ɫ
    /// </summary>
    public void InitialLevelData()
    {
        // ��ʼ��ǰ3���½ڰ�ťΪ����״̬����ɫ��
        for (int i = 0; i < 3; i++)
        {
            var chapterBtn = ChapterPanel.transform.GetChild(i);
            chapterBtn.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.3f); // �½ڰ�ť(С����)��͸��
            chapterBtn.GetComponent<Button>().interactable = false; // ���ý���

            var perLevel = transform.GetChild(2 + i);//2Ϊ�㼶��ƫ��
            //��ʼ��ÿ��С�ص�ѡ��ťΪ���ɼ�
            for(int j=0;j< 4; j++){ 
                perLevel.GetChild(j).gameObject.SetActive(false);
            }
        }

        // �������йؿ���������
        foreach (var data in LevelChooseManager.Instance.LevelRecords.LevelDatas)
        {
            // �����ؿ����Ƹ�ʽ��ʾ����"Level1-3" -> �½�1 �ؿ�3��
            string[] chapter_levelInfo = data.LevelName.Replace("Level", "").Split("-");
            int chapter = int.Parse(chapter_levelInfo[0]);
            int level = int.Parse(chapter_levelInfo[1]);

            if (data.Accessable) // ����ؿ��ѽ���
            {

                // �����Ӧ�½ڰ�ť
                var chapterBtn = ChapterPanel.transform.GetChild(chapter - 1);
                chapterBtn.GetChild(0).GetComponent<Image>().color = Color.white;    // ��ɫ
                chapterBtn.GetComponent<Button>().interactable = true;  // ���ý���

                // �����Ӧ�ؿ���ť��+1ƫ����������UI�㼶�ṹ��
                Transform levelBtn = transform.GetChild(chapter + 1).GetChild(level - 1);
                levelBtn.GetComponent<Image>().color = Color.white;
                levelBtn.GetComponent<Button>().interactable = true;
            }
            else // �ؿ�δ����
            {
                // ���ö�Ӧ�ؿ���ť
                Transform levelBtn = transform.GetChild(chapter + 1).GetChild(level - 1);
                levelBtn.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                levelBtn.GetComponent<Button>().interactable = false;
            }
        }
    }

    /// <summary>
    /// ������С������ťʱ��֮սʱ��ǰ�½ڵĹؿ�ѡ��ť��������ڵ�����UI��ʾ����ȷ
    /// </summary>
    public void ShowLevelBtn(int chapter){
        // �ر������½ڵĹؿ�ѡ��ť
        Transform perLevel;
        for (int i=1;i <= 3;i++){
            if (i == chapter) continue;
            perLevel = transform.GetChild(1 + i);//1Ϊ�㼶��ƫ��
            for (int j = 0; j < 4; j++) {
                perLevel.GetChild(j).gameObject.SetActive(false);
            }
        }
        
        // ���ǰ�½ڵĹؿ�ѡ��ť
        perLevel = transform.GetChild(1 + chapter);//1Ϊ�㼶��ƫ��
        for (int j = 0; j < 4; j++) {
            perLevel.GetChild(j).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ��¼��ǰѡ��Ĺؿ�����
    /// </summary>
    public void ChooseLevel(int level)
    {
        currentChooseLevel = level;
    }

    /// <summary>
    /// ��¼��ǰѡ����½�����
    /// </summary>
    public void ChooseChapter(int chapter)
    {
        currentChooseChapter = chapter;
    }

    /// <summary>
    /// ������Ϸ��������
    /// ����ѡ����½ڹؿ����ɳ������ƣ�ʾ����"Level2-1"��
    /// </summary>
    public void StartGame()
    {
        string sceneName = $"Level{currentChooseChapter}-{currentChooseLevel}";
        Cover.Instance.ChangeScene(sceneName); // ͨ���������ɹ��������س���
    }
}
