using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{
    private const int reflectN = 3;

    private Rigidbody2D rb;
    private Vector2 MousePos;
    public Vector2 start_pos;

    public GameObject Ballspr;
    public GameObject[] ArrowSpr = new GameObject[reflectN];

    public Text oppText;
    public int opportunity;
    public GameObject ReBtn;

    public Camera MainCam;
    
  


    public int opp;

    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        opp = 5;
        rb = GetComponent<Rigidbody2D>();
        start_pos = transform.position;
        opportunity = opp;
        UpdateText();
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {

        // circlecast 사용

        if(Input.GetMouseButtonDown(0) && opportunity > 0 && !isShooting)
        {
            for(int i = 0; i < ArrowSpr.Length; i++)
            {
                ArrowSpr[i].SetActive(true);
            }

        }

        if(Input.GetMouseButton(0) && opportunity > 0 && !isShooting)
        {
            MousePos = Input.mousePosition;
            MousePos = MainCam.ScreenToWorldPoint(MousePos);
            Vector2 Dir = MousePos - (Vector2)transform.position;
            Dir.Normalize();

            float maxDistance = 50f;
            Vector2 myposition = transform.position;
            RaycastHit2D rh2d = Physics2D.CircleCast(myposition, 0.18f, Dir, maxDistance);
           
            
            ArrowSpr[0].transform.position = transform.position;
            ArrowSpr[0].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90 + Mathf.Atan2(Dir.y, Dir.x) * 180 / Mathf.PI));

            
            
            //normal을 써줘야함. (법선)
            Vector2 Dir2 = Vector3.Reflect(Dir, rh2d.normal.normalized);
            Dir2.Normalize();
            RaycastHit2D rh2d2 = Physics2D.CircleCast(rh2d.point + rh2d.normal.normalized * 0.18f, 0.18f, Dir2, maxDistance);
            ArrowSpr[1].transform.position = rh2d.point + rh2d.normal.normalized * 0.18f;
            ArrowSpr[1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90 + Mathf.Atan2(Dir2.y, Dir2.x) * 180 / Mathf.PI));

            Vector2 Dir3 = Vector3.Reflect(Dir2, rh2d2.normal.normalized);
            Dir3.Normalize();
            RaycastHit2D rh2d3 = Physics2D.CircleCast(rh2d2.point + rh2d2.normal.normalized * 0.18f, 0.18f, Dir3, maxDistance);
            ArrowSpr[2].transform.position = rh2d2.point + rh2d2.normal.normalized * 0.18f;

            ArrowSpr[2].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90 + Mathf.Atan2(Dir3.y, Dir3.x) * 180 / Mathf.PI));


        }

        if (Input.GetMouseButtonUp(0) && opportunity > 0 && !isShooting)
        {
            MousePos = Input.mousePosition;
            MousePos = MainCam.ScreenToWorldPoint(MousePos);
            Vector2 Dir = MousePos - (Vector2)transform.position;
            Dir.Normalize();
            Shooting(Dir);

            opportunity--;
            UpdateText();
            isShooting = true;
            
            for (int i = 0; i < ArrowSpr.Length; i++)
            {
                ArrowSpr[i].SetActive(false);
            }


        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
            isShooting = false;

        }

    }


    public void UpdateText()
    {
        oppText.text = opportunity.ToString();
    }

    public void Reset()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = start_pos;
    }

    public void ResetOpp()
    {
        opportunity = opp;
    }


    public void ONReBtn()
    {
        ReBtn.SetActive(true);
    }

    public void setIsShooting(bool shooting)
    {
        isShooting = shooting;
    }

    public bool getIsShooting()
    {
        return isShooting;
    }

    void Shooting(Vector2 Dir)
    {
        rb.AddForce(new Vector2(9.8f * 25f * Dir.x, 9.8f * 25f * Dir.y));
    }



}
