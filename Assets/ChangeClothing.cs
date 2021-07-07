using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothing : MonoBehaviour
{
    public GameObject playerCloth;
    private Rigidbody rigidbody;
    void Start()
    {
        playerCloth = gameObject.GetComponent<GameObject>();

        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // �浹�� ������Ʈ �±װ� Item �̸� ����
        if (other.gameObject.tag.Equals("Item"))
        {
            Debug.Log("123");

            // �̸��� body�� ������Ʈ�� ã�´�
            GameObject obj1 = transform.Find("Body").gameObject; // Object�� �̸��� ã��. ���� ó���� ������ Object�� ��ȯ.

            for (int j = 0; j < other.transform.childCount; j++)
            {
                // body�� �ڽ� ������Ʈ ������ŭ �ݺ�
                for (int i = 0; i < obj1.transform.childCount; i++)
                {
                    // �ڽ� ������Ʈ ����
                    GameObject obj2 = obj1.transform.GetChild(j).gameObject; // �ڽ��� ��ȣ�� ã��. 0��°�� ù ��° �ڽ�
                    GameObject activeItem = other.transform.GetChild(i).gameObject;

                    // �浹�� �Ͱ� �˻��� ������Ʈ�� �̸��� �ٸ��� , �ش� �������� Ȱ��ȭ �Ǿ��־��ٸ�
                    // �÷��̾��� ���� ���������� �ٲٰ� �������� �÷��̾��� ������ �ٲ�
                    if (activeItem.activeSelf == true && activeItem.name != obj2.name)
                    {
                        obj2.SetActive(true);
                        activeItem.SetActive(true);

                    }
                    else
                    {
                        obj2.SetActive(false);
                        activeItem.SetActive(false);
                    }
                }
            }

            
            
        }


    }

}
