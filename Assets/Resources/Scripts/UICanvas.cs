using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UICanvas : MonoBehaviour
{
    public Ball ball;
    public GameObject ReBtn;
    public GameObject ClearUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReButton()
    {
        SceneManager.LoadScene("GameScene");

        //ball.Reset();
        //ReBtn.SetActive(false);
        //ClearUI.SetActive(false);
        //ball.ResetOpp();
        //ball.UpdateText();
    }

    public void NextButton()
    {
        if(GameMng.instance.now_stage == GameMng.instance.clear_stage + 1)
        {
            GameMng.instance.clear_stage += 1;
            PlayerPrefs.SetInt("clear_stage", GameMng.instance.clear_stage);
        }
        GameMng.instance.now_stage++;
        SceneManager.LoadScene("GameScene");
    }


}
