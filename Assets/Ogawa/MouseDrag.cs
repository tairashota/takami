using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    float speed = 0;
    Vector2 startPos;
    private Vector3 offset;
    public GameObject panel1, panel2, panel3;
    private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        // mainCamera = Camera.main; // ÉÅÉCÉìÉJÉÅÉâÇéÊìæ
        Application.targetFrameRate = 60;
    }

    // Update is called one per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Input.mousePosition;
            float swipeLength = endPos.x - this.startPos.x;

            this.speed = swipeLength / 50.0f;
        }

        if (startPos.y <= 1250*0.3839 && startPos.y >= 970 * 0.3839)
        {
            if (panel1.transform.position.x < 0)
            {
                this.speed = 0f;
                panel1.transform.position = new Vector3(0, panel1.transform.position.y, 0);
            }
            else if (panel1.transform.position.x > 875 * 0.3839)
            {
                this.speed = 0f;
                panel1.transform.position = new Vector3(875, panel1.transform.position.y, 0);
            }
            panel1.transform.Translate(this.speed, 0, 0);
            this.speed *= 0.96f;



        }




        if (startPos.y <= 850 && startPos.y >= 570)
        {
            if (panel2.transform.position.x < 0)
            {
                this.speed = 0f;
                panel2.transform.position = new Vector3(0, panel2.transform.position.y, 0);
            }
            else if (panel2.transform.position.x > 875)
            {
                this.speed = 0f;
                panel2.transform.position = new Vector3(875, panel2.transform.position.y, 0);
            }
            panel2.transform.Translate(this.speed, 0, 0);
            this.speed *= 0.96f;



        }
        if (startPos.y <= 450 && startPos.y >= 170)
        {

            if (panel3.transform.position.x < 0)
            {
                this.speed = 0f;

                panel3.transform.position = new Vector3(0, panel3.transform.position.y, 0);
            }
            else if (panel3.transform.position.x > 875)
            {
                this.speed = 0f;
                panel3.transform.position = new Vector3(875, panel3.transform.position.y, 0);
            }
            panel3.transform.Translate(this.speed, 0, 0);
            this.speed *= 0.96f;

        }


    }
}