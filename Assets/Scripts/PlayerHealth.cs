using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;              //�ִ� �����
    public int currentLives;              //���� �����

    public float invincibleTime = 1.0f;           //�ǰ� �� ���� �ð�(�ݺ� �ǰ� ����)
    public bool isInvincible = false;             //���� ������ ��

    void Start()
    {
        currentLives = maxLives;               //����� �ʱ�ȭ
    }

    private void OnTriggerEnter(Collider other)        //Ʈ���� ���� �ȿ� ���Գ� �˻��ϴ� �Լ�
    {
        if (other.CompareTag("Missile"))     //�̻��ϰ� �浹 �ϸ�
        {
            currentLives--;
            Destroy(other.gameObject);          //�̻��� ������Ʈ�� ���ش�.

            if (currentLives <=0)        //���� ü���� 0 ������ ���
            {
                GameOver();
            }
        }
    }

    void GameOver()           //���� ���� ó��
    {
        //�÷��̾� ��Ȱ��ȭ
        gameObject.SetActive(false);          //�÷��̾ �Ⱥ��δ�.
        Invoke("RestartGame", 3.0f);          //3�� �� ���� �� �����
    }

    void RestartGame()
    {
        //���� �� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene() .name);
    }
}
