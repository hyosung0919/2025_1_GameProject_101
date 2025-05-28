using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleBallController : MonoBehaviour
{
    [Header("�⺻ ����")]
    public float power = 10f;                   //Ÿ�� ��
    public Sprite arrowSprite;                  //ȭ��ǥ �̹���

    private Rigidbody rb;                       //���� ����
    private GameObject arrow;                   //ȭ��ǥ ������Ʈ
    private bool isDragging = false;            //�巡�� ������ Ȯ���ϴ� Bool
    private Vector3 startPos;                   //�巡�� ���� ��ġ

    void Start()
    {
        SetupBall();
    }

    void Update()
    {
        HandleInput();
        UpdateArrow();
    }

    void SetupBall()                            //�� �����ϱ�
    {
        rb = GetComponent<Rigidbody>();                         //���� ������Ʈ ��������
        if (rb == null )
        {
            rb = gameObject.AddComponent<Rigidbody>();          //���� ��� �ٿ��ش�,
        }

        //���� ����
        rb.mass = 1;
        rb.drag = 1;
    }

    public bool isMoving()                                      //���� �����̰� �ִ��� Ȯ��
    {
        return rb.velocity.magnitude > 0.2f;                    //���� �ӵ��� ������ ������ �����δٰ� �Ǵ�
    }

    void HandleInput()
    {
        if (!SimpleTurnManager.canPlay) return;                 //�� �Ŵ��� ��� ���� ������ ���� �Ұ�
        if(SimpleTurnManager.anyBallMoving) return;             //�ٸ� ���� ������ �� ���� �Ұ�

        if(isMoving()) return;                                  //���� �����̰� ������ ���� �Ұ�

        if(Input.GetMouseButtonDown(0))                         //���콺 Ŭ�� ����
        {
            StartDrag();
        }

        if(Input.GetMouseButtonUp(0) && isDragging)             //�巡�� ���̿��µ� ���콺 ��ư �� ���� ��
        {
            Shoot();
        }

    }

    void Shoot()                                        //�� �߻� �ϱ�
    {
        Vector3 mouseDelta = Input.mousePosition - startPos;            //���콺 �̵� �Ÿ��� �� ���
        float force = mouseDelta.magnitude * 0.01f * power;

        if (force < 5) force = 5;                                       //�ּ� �� ���� �� ����

        Vector3 direction = new Vector3(-mouseDelta.x, 0, - mouseDelta.y).normalized;     //���� ���

        rb.AddForce(direction * force, ForceMode.Impulse);              //���� �� ����

        SimpleTurnManager.OnBallHit();                      //�� �Ŵ������� ���� �ƴٰ� �˸�

        //�� �߻� ���� ������ ����
        isDragging = false ;
        Destroy(arrow);
        arrow = null;

        Debug.Log("�߻�! �� : " + force);
    }

    void CreateArrow()                         //ȭ��ǥ �����
    {
        if(arrow == null)
        {
            Destroy(arrow);                     //������ ȭ��ǥ�� ���� ��� ����

        }

        arrow = new GameObject("Arrow");        //(�� ������Ʈ ���� -> new GameObject(�̸�))�� ȭ��ǥ �����
        SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();           //���� ���� ������Ʈ�� �������� ���δ�

        sr.sprite = arrowSprite;
        sr.color = Color.green;
        sr.sortingOrder = 10;

        arrow.transform.position = transform.position + Vector3.up; //ȭ��ǥ ��ġ�� ����ش�.
        arrow.transform.localScale = Vector3.one;
    }

    void UpdateArrow()
    {
        if(!isDragging || arrow == null) return;

        Vector3 mouseDelta = Input.mousePosition - startPos;        //���콺 �̵� �Ÿ� ���
        float distance = mouseDelta.magnitude;

        float size = Mathf.Clamp(distance * 0.01f, 0.5f, 2.0f);     //ȭ��ǥ ũ�⸦ ���� ���� ����
        arrow.transform.localScale = Vector3.one * size;

        SpriteRenderer sr = arrow.GetComponent<SpriteRenderer>();   //ȭ��ǥ ������ �ʷ� -> ����
        float colorRatio = Mathf.Clamp01(distance * 0.005f);
        sr.color = Color.Lerp(Color.green, Color.red, colorRatio);  //�̵� �Ÿ��� ����� ���� �ʷϿ��� �������� ���Ѵ�.

        if(distance > 10f)                                          //�ּ� �Ÿ� �̻� �巡�� ���� ��
        {
            Vector3 direction = new Vector3(-mouseDelta.x, 0, -mouseDelta.y);

            //2D��� (������ �� ����) ���� drection ���Ͱ� ����Ű�� ������ ������ ��ȯ
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;        //���� ������ ��ȯ �����ִ� ����
            arrow.transform.rotation = Quaternion.Euler(90, angle, 0);                  //ȭ��ǥ ������ ���� �Ѵ�.
        }
    }

    void StartDrag()                                     //�巡�� ���� �Լ�
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        //ȭ�鿡�� ray�� ����
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))                                   //��Ʈ �� ���� ���� ���
        {
            if(hit.collider.gameObject == gameObject)                       //�ش� ������Ʈ�� �ڽ��� ���
            {
                isDragging = true;                                          //�巡�� ���� ����
                startPos = Input.mousePosition;                             //���� ��ġ ����
                CreateArrow();                                              //ȭ��ǥ ���� �Լ� ȣ��
                Debug.Log("�巡�� ����");
            }
        }
    }
}
