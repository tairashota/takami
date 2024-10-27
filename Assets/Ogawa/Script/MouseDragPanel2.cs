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

    public List<GameObject> pages; // ���I�Ƀy�[�W���i�[���郊�X�g
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

        // �}�E�X�̈ʒu���擾
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        Vector3 newPos = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
        transform.position = newPos; // �p�l���̈ʒu���X�V
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        // mainCamera = Camera.main; // ���C���J�������擾
        Application.targetFrameRate = 60;


        // �q�I�u�W�F�N�g���擾����
        
    }

    private static Transform[] GetChildren(Transform parent)
    {
        // �q�I�u�W�F�N�g���i�[����z��쐬
        var children = new Transform[parent.childCount];

        // 0�`��-1�܂ł̎q�����Ԃɔz��Ɋi�[
        for (var i = 0; i < children.Length; ++i)
        {
            children[i] = parent.GetChild(i);
        }

        // �q�I�u�W�F�N�g���i�[���ꂽ�z��
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

            // �E���̐�����ύX���邱�ƂŁA��葽����������
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
            if (swipeLength > 50 && currentPage < pages.Count - 1)  // �E�X���C�v
            {
                currentPage++;
                ShowPage(currentPage);
            }
            else if (swipeLength < -50 && currentPage > 0) // ���X���C�v
            {
                currentPage--;
                ShowPage(currentPage);
            }
        }
        

        void ShowPage(int pageIndex)
        {
            // �y�[�W��\�����郍�W�b�N
            Debug.Log($"Showing page: {pageIndex}");
            // ������UI���X�V���鏈����ǉ����܂�
            // ��: panels.transform.position = new Vector3(-pageIndex * panelWidth, panels.transform.position.y, 0);
        }

*/

        void OnClick()
        {
            // �J�ڂ��������V�[���̖��O���uNext�v�̏ꍇ
            SceneManager.LoadScene("iwakinoYouScene");
        }
    }
}


