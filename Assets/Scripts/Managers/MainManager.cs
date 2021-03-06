using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    static MainManager m_instance;
    static MainManager Instance { get { Init(); return m_instance; } }

    #region Contents
    GameManager _game = new GameManager();

    public static GameManager Game { get { return Instance._game; } }
    #endregion

    #region Core
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    AudioManager _audio = new AudioManager();

    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static AudioManager Audio { get { return Instance._audio; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    static void Init()
    {
        if (m_instance == null)
        {
            GameObject go = GameObject.Find("@MainManager");
            if (go == null)
            {
                go = new GameObject { name = "@MainManager" };
                go.AddComponent<MainManager>();
            }

            DontDestroyOnLoad(go);
            m_instance = go.GetComponent<MainManager>();
        }

        m_instance._pool.Init();
        m_instance._audio.Init();
    }
}
