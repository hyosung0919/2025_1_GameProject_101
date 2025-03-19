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
        if (Input.GetMouseButtonDown(0))       //마우스 버튼을 누르면
        {
            RaycastHit hit;                //Ray 선언
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);           //카메라에서 레이저를 쏴서 검출한다.

            if (Physics.Raycast(ray, out hit))              //Hit 된 오브젝트를 검출한다.
            {
                if (hit.collider != null)          //Hit된 오브젝트가 있을 경우
                {
                    // Debug.Log(hit.collider.name);           //로그로 보여준다.
                    hit.collider.gameObject.GetComponent<Monster>().CharacterHit(10);      //몬스터에게 데미지 10을 준다.
                }
            }
        }
    }
}