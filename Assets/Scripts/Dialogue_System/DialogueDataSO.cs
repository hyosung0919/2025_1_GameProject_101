using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue" , menuName = " Dialogue/DialogueData")]
public class DialogueDataSO : ScriptableObject
{
    [Header("ĳ���� ����")]                                      //��ȭ â�� ǥ�õ� ĳ���� �̸�
    public string characterName = "ĳ����";                      //ĳ���� �� �̹���
    public Sprite characterImage;

    [Header("��ȭ ����")]
    [TextArea(3,10)]                                                //Inspector ���� ���� �� �Է� �����ϰ� ����
    public List<string> dialougeLines = new List<string>();         //��ȭ ����� (������� ��µ�
}
