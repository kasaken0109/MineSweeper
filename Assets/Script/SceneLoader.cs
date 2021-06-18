using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移を管理する
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>ロードするシーン名/// </summary>
    [SerializeField] string m_loadScene = "MineSweeper";
    /// <summary>ロードまでの待機時間/// </summary>
    [SerializeField] float m_waitingTime = 3f;
    /// <summary>カウント用の変数/// </summary>
    float m_time = 0;
    /// <summary>ロードするか判断する/// </summary>
    bool Isload = false;
  
    // Update is called once per frame
    void Update()
    {
        //ロードするとき待機時間まで処理を遅らせる
        if (Isload)
        {
            m_time += Time.deltaTime;
            if (m_time >= m_waitingTime)
            {
                SceneManager.LoadScene(m_loadScene);
            }
        }
    }

    /// <summary>
    /// ボタンに使うシーン読み込みを行う
    /// </summary>
    public void SelectSceneLoad()
    {
        Isload = true;
    }

    /// <summary>
    /// シーンを再読み込みする
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
