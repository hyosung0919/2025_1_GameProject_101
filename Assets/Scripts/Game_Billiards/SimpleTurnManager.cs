using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurnManager : MonoBehaviour
{
    //���� ���� (��� ���� ���� �ؼ� ��� �� �� ����)
    public static bool canPlay = true;                          //���� ĥ �� �ִ���
    public static bool anyBallMoving = false;                   //� ���̶� �����̰� �ִ���
    void Start()
    {
        
    }

    void Update()
    {
        CheckAllBalls();                                    //��� ���� ������ Ȯ�� ȣ��

        if(!anyBallMoving && !canPlay)
        {
            canPlay = true;                                 //��� ���� ���߸� �ٽ� ĥ �� �ְ� ��
            Debug.Log("�� ���� ! �ٽ� ĥ �� �ֽ��ϴ�.");
        }
    }

    void CheckAllBalls()                    //��� ���� ������� Ȯ��
    {
        SimpleBallController[] allballs = FindObjectsOfType<SimpleBallController>();        //Scene�� �ִ� SimpleBallController�� ��� �ϴ� ��� ������Ʈ�� �迭�� �ִ´�.
        anyBallMoving = false;                              //�ʱ�ȭ �����ش�

        foreach(SimpleBallController ball in allballs)      //�迭 ��ü Ŭ������ ��ȯ �ϸ鼭
        {
            if(ball.isMoving())                             //���� �����̰� �ִ��� Ȯ�� �ϴ� �Լ��� ȣ��
            {
                anyBallMoving = true;                       //���� �����δٰ� ���� ����
                break;                                      //�������� ���� ���´�.
            }
        }
    }

    public static void OnBallHit()                          //���� �÷��� ���� �� ȣ��
    {
        canPlay = false;                                    //�ٸ� ������ �� �����̰� ��
        anyBallMoving = true;                               //���� �����̱� �����ϱ� ������ bool �� ����
        Debug.Log("�� ����! ���� ���� �� ���� ��ٸ�����.");
    }
}
