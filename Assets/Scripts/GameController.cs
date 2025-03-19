using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float GameTimer = 3.0f;
    public GameObject MonsterGO;

    // Update is called once per frame
    void Update()
    {
        GameTimer -= Time.deltaTime;

        if (GameTimer <= 0)
        {
            GameTimer = 3.0f;

            GameObject Temp = Instantiate(MonsterGO);
            Temp.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4), 0.0f);

        }
        if (Input.GetMouseButtonDown(0))       //���콺 ��ư�� ������
        {
            RaycastHit hit;                //Ray ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);           //ī�޶󿡼� �������� ���� �����Ѵ�.

            if (Physics.Raycast(ray, out hit))              //Hit �� ������Ʈ�� �����Ѵ�.
            {
                if (hit.collider != null)          //Hit�� ������Ʈ�� ���� ���
                {
                    // Debug.Log(hit.collider.name);           //�α׷� �����ش�.
                    hit.collider.gameObject.GetComponent<Monster>().CharacterHit(10);      //���Ϳ��� ������ 10�� �ش�.
                }
            }
        }
    }
}