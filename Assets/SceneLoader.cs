using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string m_loadScene = "MineSweeper";
    bool Isload = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Isload)
        {
            SceneManager.LoadScene(m_loadScene);
        }
    }

    public void SelectSceneLoad()
    {
        Isload = true;
    }
}
