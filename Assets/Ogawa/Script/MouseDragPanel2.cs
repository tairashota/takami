using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static SearchEngineDire;

public class MouseDragPanel2: MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] float extRate = 1.1f;
    [SerializeField] float time = 0.2f;

    public List<GameObject> pages; // 動的にページを格納するリスト
    private int currentPage = 0;
    [SerializeField] private Transform _parent;

    public static List<serachResult> serachResultsList;

    float speed = 0;
    Vector2 startPos;
    private Vector3 offset;
    public GameObject panels;
    private Camera mainCamera;
    int n;


    void OnMouseDown()
    {

        Debug.Log("Mouse Down");

        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    void OnMouseDrag()
    {

        Debug.Log("Mouse Drag");

        // マウスの位置を取得
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        Vector3 newPos = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
        transform.position = newPos; // パネルの位置を更新
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        // mainCamera = Camera.main; // メインカメラを取得
        Application.targetFrameRate = 60;


        // 子オブジェクトを取得する
        
    }

    private static Transform[] GetChildren(Transform parent)
    {
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



    // Update is called once per frame
    void Update()
    {
        var children = GetChildren(_parent);
        float n = 450 * 0.3839f - (children.Length-1)*900 * 0.3839f;
        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Input.mousePosition;
            float swipeLength = endPos.x - this.startPos.x;

            this.speed = swipeLength / 25.0f;
           
            Debug.Log("Mouse 2");
        }




        if (startPos.y <= 1550 * 0.3839f && startPos.y >= 300 * 0.3839f)
        {
            LimitPanelPosition();
            Debug.Log(speed);
            panels.transform.Translate(speed, 0, 0);
            speed *= 0.96f;
                 }
         void LimitPanelPosition()
        {
            /*
            float minX = -((pages.Count - 1) * panels.GetComponent<RectTransform>().rect.width);
            float maxX = 0;

            // 右側の制限を変更することで、より多く動かせる
            if (panels.transform.position.x < minX)
            {
                panels.transform.position = new Vector3(minX, panels.transform.position.y, panels.transform.position.z);
                speed = 0f;
            }
            else if (panels.transform.position.x > maxX)
            {
                panels.transform.position = new Vector3(maxX, panels.transform.position.y, panels.transform.position.z);
                speed = 0f;
            }
            */

            if (panels.transform.position.x < n)
            {
                speed = 0f;
                panels.transform.position = new Vector3(n, panels.transform.position.y, 0);
            }
            else if (panels.transform.position.x > 450 * 0.3839f)
            {
                speed = 0f;
                panels.transform.position = new Vector3(450 * 0.3839f, panels.transform.position.y, 0);
            }
        }

        /*
        void UpdatePage(float swipeLength)
        {
            if (swipeLength > 50 && currentPage < pages.Count - 1)  // 右スワイプ
            {
                currentPage++;
                ShowPage(currentPage);
            }
            else if (swipeLength < -50 && currentPage > 0) // 左スワイプ
            {
                currentPage--;
                ShowPage(currentPage);
            }
        }
        

        void ShowPage(int pageIndex)
        {
            // ページを表示するロジック
            Debug.Log($"Showing page: {pageIndex}");
            // ここでUIを更新する処理を追加します
            // 例: panels.transform.position = new Vector3(-pageIndex * panelWidth, panels.transform.position.y, 0);
        }

*/

        void OnClick()
        {
            // 遷移させたいシーンの名前が「Next」の場合
            SceneManager.LoadScene("iwakinoYouScene");
        }
    }
}


