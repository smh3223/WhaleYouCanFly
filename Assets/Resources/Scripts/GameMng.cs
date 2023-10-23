using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    public static GameMng instance = null;

    public int now_stage;
    public int clear_stage;
    void Awake()
    {
        now_stage = PlayerPrefs.GetInt("now_stage", 1);
        clear_stage = PlayerPrefs.GetInt("clear_stage", 0);
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
