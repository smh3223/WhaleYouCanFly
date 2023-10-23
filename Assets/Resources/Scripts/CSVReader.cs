using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
/// <summary>
/// CSV Reader
/// * Resources/CSV/[path] csv파일 저장
/// * OnLoadCSV(path) Method csv파일 로드
/// * OnLoadTextAsset(data) 로드된 csv TextAsset데이터 파싱
/// </summary>
public class CSVReader : MonoBehaviour
{
    public GameObject Ball;
    public GameObject WBlock;
    public GameObject star;

    public GameObject ClearUI;

    public int StarCount;

    public Sprite starSprite;

    private int StageNum;
    private Vector2 MapSize;
    private Vector2 MapPoint;

    private void Start()
    {
        StarCount = 0;

        OnLoadCSV("stage" + GameMng.instance.now_stage);
        PlayerPrefs.SetInt("now_stage", GameMng.instance.now_stage);
    }

    public void OnLoadCSV(string path)
    {
        string file_path = "CSV/";
        file_path = string.Concat(file_path, path);
        TextAsset ta = Resources.Load(file_path, typeof(TextAsset)) as TextAsset;
        OnLoadTextAsset(ta.text);

        Resources.UnloadAsset(ta);
        ta = null;
    }

    public void OnLoadTextAsset(string data)
    {
        string[] str_lines = data.Split('\n');
        
        string[] d = str_lines[1].Split(',');

        StageNum = int.Parse(d[0]);
        MapSize.x = int.Parse(d[1]);
        MapSize.y = int.Parse(d[2]);

        MapPoint.x = (4.8f / MapSize.x);
        MapPoint.y = (9.5f / MapSize.y);
        

        for (int i = 3; i < str_lines.Length - 1; ++i) // 행
        {
            string[] values = str_lines[i].Split(',');
            for (int j = 0; j < values.Length ; j++)         // 열
            {
                if (!string.IsNullOrEmpty(values[j]) && values[j] != "player" && values[j] != "\r")
                {
                    string[] stringdatas = values[j].Split('/');
                    int[] datas = new int[5];

                    for (int k = 0; k < stringdatas.Length; k++)
                    {
                        
                        if (stringdatas[k] == "x")
                        {
                            datas[k] = (int)MapSize.x;
                        }
                        else if (stringdatas[k] == "y")
                        {
                            datas[k] = (int)MapSize.y;
                        }
                        else
                        {
                            datas[k] = int.Parse(stringdatas[k]);
                            
                            //int result;
                            //if (int.TryParse(stringdatas[k], out result)) { datas[k] = result; }
                        }
                        
                    }



                    CreateObject(j, i - 3, datas[0], datas[1], datas[2], datas[3], datas[4]);
                }

                else if (values[j] == "player")
                {
                    Vector2 pos = new Vector2(-2.4f + j * MapPoint.x, 5f - (i-4) * MapPoint.y);
                    Ball.transform.position = pos;
                }

            }
        }

    }

    public void CreateObject(int x, int y, int color, int xScale, int yScale, int Reverse, int Mirror)
    {
        Vector2 pos = new Vector2(-2.4f + x * MapPoint.x, 5f - y * MapPoint.y);

        GameObject InsBlock;
        
        if(color > 3)
        {
            InsBlock = Instantiate(star);
            color -= 3;
            StarCount += 1;
        }
        else
        {
            InsBlock = Instantiate(WBlock);
        }

        InsBlock.transform.position = pos;
        SpriteRenderer spr = InsBlock.GetComponent<SpriteRenderer>();

        switch (color)
        {
            case 1:
                spr.color = Color.magenta;
                break;
            case 2:
                spr.color = Color.green;
                break;
            case 3:
                spr.color = Color.cyan;
                break;

        }

        InsBlock.transform.localScale = new Vector3((float)xScale / (MapSize.x/10) / 2.0f, (float)yScale/ (MapSize.y/10), 1);

        InsBlock.transform.rotation = Quaternion.Euler(InsBlock.transform.rotation.x, InsBlock.transform.rotation.y,
            InsBlock.transform.rotation.z - (Reverse-1) * 90.0f);
    }

    public void EatingStar()
    {
        StarCount--;

        if (StarCount == 0)
        {
            Clear();
        }
    }

    public int GetStarCount()
    {
        return StarCount;
    }


    public void Clear()
    {
        Debug.Log("claer");
        ClearUI.SetActive(true);
    }

}