using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragPaenl : MonoBehaviour
{
    float speed = 1;
    Vector2 startPos;
    private Vector3 offset;
    private Camera mainCamera;

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
        // mainCamera = Camera.main; // メインカメラを取得
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
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


        transform.Translate(this.speed, 0, 0);
        this.speed *= 0.96f;
    }
}

