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
        
        // 충돌한 오브젝트 태그가 Item 이면 시작
        if (other.gameObject.tag.Equals("Item"))
        {
            Debug.Log("123");

            // 이름이 body인 오브젝트를 찾는다
            GameObject obj1 = transform.Find("Body").gameObject; // Object의 이름을 찾음. 가장 처음에 나오는 Object를 반환.

            for (int j = 0; j < other.transform.childCount; j++)
            {
                // body의 자식 오브젝트 갯수만큼 반복
                for (int i = 0; i < obj1.transform.childCount; i++)
                {
                    // 자식 오브젝트 선언
                    GameObject obj2 = obj1.transform.GetChild(j).gameObject; // 자식을 번호로 찾음. 0번째가 첫 번째 자식
                    GameObject activeItem = other.transform.GetChild(i).gameObject;

                    // 충돌한 것과 검색한 오브젝트가 이름이 다르고 , 해당 아이템이 활성화 되어있었다면
                    // 플레이어의 옷을 아이템으로 바꾸고 아이템은 플레이어의 옷으로 바꿈
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
