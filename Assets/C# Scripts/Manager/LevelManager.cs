using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public void FinishLevel()
    {
        AudioManager.Instance.PlaySe("FinishLevel");

        string currentScene = SceneManager.GetActiveScene().name;
        string[] sceneParts = currentScene.Split('-');

        // �����ؿ�����
        if (sceneParts.Length >= 2 && int.TryParse(sceneParts.Last(), out int currentLevel))
        {
            // ������һ������
            string baseName = string.Join("-", sceneParts.Take(sceneParts.Length - 1));
            string nextScene = $"{baseName}-{currentLevel + 1}";

            // ��鳡���Ƿ����
            if (SceneExists(nextScene))
            {
                Cover.Instance.ChangeScene(nextScene);
                return;
            }
        }

        // û����һ��ʱ����ѡ�ؽ���
        Cover.Instance.ChangeScene("ChooseLevelMenu");
    }

    public void RestartLevel()
    {
        Cover.Instance.ChangeScene(SceneManager.GetActiveScene().name);
    }
    // ��鳡���Ƿ������Build Settings
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var lastSlash = scenePath.LastIndexOf("/");
            var sceneNameInBuild = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".unity") - lastSlash - 1);

            if (sceneNameInBuild == sceneName)
                return true;
        }
        return false;
    }


}
