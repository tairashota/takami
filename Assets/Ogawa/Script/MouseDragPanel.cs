using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static SearchEngineDire;

public class MouseDragPanel : MonoBehaviour
{
    float speed = 0;
    Vector2 startPos;
    private Vector3 offset;
    public GameObject panel1, panel2, panel3;
    private Camera mainCamera;

   
    public List<GameObject> pages; // 動的にページを格納するリスト
    private int currentPage = 0;
    //private Transform _parent;
    [SerializeField] private Transform _parent;

    



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // メインカメラを取得
        Application.targetFrameRate = 60;
        //_parent = GameObject.Find("ParentObjectName").transform;

    }



    
    private static Transform[] GetChildren(Transform parent)
    {

        if (parent == null)
        {
            Debug.LogError("Parent transform is null");
            return new Transform[0]; // 空の配列を返す
        }

        // 子オブジェクトを格納する配列作成
        var children = new Transform[parent.childCount];

        // 0～個数-1までの子を順番に配列に格納
        for (var i = 0; i < children.Length; ++i)
        {
            children[i] = parent.GetChild(i);
        }

        // 子オブジェクトが格納された配列
        return children;
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

        if (startPos.y <= 1250 * 0.3839f && startPos.y >= 970 * 0.3839f)
        {
            if (panel1.transform.position.x < -280 * 0.3839f)
            {
                this.speed = 0f;
                panel1.transform.position = new Vector3(-280 * 0.3839f, panel1.transform.position.y, 0);
            }
           else if (panel1.transform.position.x > 875 * 0.3839f)
            {
                this.speed = 0f;
                panel1.transform.position = new Vector3(875 * 0.3839f, panel1.transform.position.y,0);
            }
                panel1.transform.Translate(this.speed, 0, 0);
                this.speed *= 0.96f;
        }




        if (startPos.y <= 850 * 0.3839f && startPos.y >= 570 * 0.3839f)
        {
            if (panel2.transform.position.x < -280 * 0.3839f)
            {
                this.speed = 0f;
                panel2.transform.position = new Vector3(-280 * 0.3839f, panel2.transform.position.y, 0);
            }
            else if (panel2.transform.position.x > 875 * 0.3839f)
            {
                this.speed = 0f;
                panel2.transform.position = new Vector3(875 * 0.3839f, panel2.transform.position.y, 0);
            }
            panel2.transform.Translate(this.speed, 0, 0);
            this.speed *= 0.96f;
        }





        if (startPos.y <= 450 * 0.3839f && startPos.y >= 170 * 0.3839f)
        {

            var children = GetChildren(_parent);
            float n = 0.0f;//= 450 * 0.3839f - (children.Length - 1) * 900 * 0.3839f;

            if (children.Length <= 4)
            {
                n = 875 * 0.3839f;
            }
            else
            {
                n = 290.0f - (children.Length-5)*85.0f;
            }
            
            Debug.Log(n);

            // パネル3の位置制限
            if (panel3.transform.position.x < n)
            {
                this.speed = 0f;
                panel3.transform.position = new Vector3(n, panel3.transform.position.y, 0);
            }

            if (panel3.transform.position.x > 875 * 0.3839f)
            {
                this.speed = 0f;
                panel3.transform.position = new Vector3(875 * 0.3839f, panel3.transform.position.y, 0);
            }
            panel3.transform.Translate(this.speed, 0, 0);
            this.speed *= 0.96f;

        }
            


        /*
         
        if (startPos.y <= 450 * 0.3839f && startPos.y >= 170 * 0.3839f)
        {
            LimitPanelPosition();
            Debug.Log(speed);
            panels.transform.Translate(speed, 0, 0);
            speed *= 0.96f;
        }

        
        if (startPos.y <= 450 * 0.3839f && startPos.y >= 170 * 0.3839f)
        {

            if (panel3.transform.position.x < -280 * 0.3839f)
            {
                this.speed = 0f;
               
                panel3.transform.position = new Vector3(-280 * 0.3839f, panel3.transform.position.y, 0);
            }
            else if (panel3.transform.position.x > 875 * 0.3839f)
            {
                this.speed = 0f;
                panel3.transform.position = new Vector3(875 * 0.3839f, panel3.transform.position.y, 0);
            }
            panel3.transform.Translate(this.speed, 0, 0);
            this.speed *= 0.96f;

        }
        */
        
    }
}