using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 3f;

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;


    bool interActionDown;
    bool prevInteraction;

    // 가까운 오브젝트 탐지용
    GameObject nearObject;

    public GameObject[] weapons;
    public bool[] hasWeapons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MoveUpdate();
        Interaction();
    }

    void GetInput()
    {
        interActionDown = Input.GetButtonDown("Interaction");
        if (prevInteraction != interActionDown)
        {
            Debug.Log("a");
        }

        prevInteraction = interActionDown;
    }

    void Interaction()
    {
        //Debug.Log("interActionDown : " + interActionDown);
        if(interActionDown && nearObject != null)
        {
            if(nearObject.CompareTag("Weapon"))
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }

    void MoveUpdate()
    {
        // -1 : a or left  1 : d or right
        float horizontal = Input.GetAxis("Horizontal");
        // -1 : w or up  1 : s or down
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            nearObject = other.gameObject;
            //Debug.Log(nearObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            nearObject = null;
            //Debug.Log("no weapon");

        }
    }
}
