using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public enum State { Idle, Move, Run };
    public CharacterController controller;
    public Transform cam;
    public float speed;

    public GameObject aimCamera;
    CinemachineFreeLook vCam;

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;

    // animation
    Animator anim;
    Animator aimCamAnim;

    bool isInterActionDown;
    bool isRunDown;
    bool isAimDown;

    public State state;

    float angle {get; set;}

    // 가까운 오브젝트 탐지용
    GameObject nearObject;

    public GameObject[] weapons;
    public bool[] hasWeapons;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetChild(1).GetComponent<Animator>();
        aimCamera = GameObject.FindGameObjectWithTag("MainCamera");
        aimCamAnim = aimCamera.GetComponent<Animator>();
        state = State.Idle;


    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Interaction();
        GunUpdate();
        switch (state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Run:
                RunUpdate();
                break;
            case State.Move:
                MoveUpdate();
                break;
        }
    }

    void GetInput()
    {
        isInterActionDown = Input.GetButtonDown("Interaction");
        isRunDown = Input.GetButton("Run");
        isAimDown = Input.GetButton("Aim");
    }

    void Interaction()
    {
        //Debug.Log("interActionDown : " + interActionDown);
        if(isInterActionDown && nearObject != null)
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
    void IdleUpdate()
	{
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
		{
            anim.SetBool("Idle", false);
            anim.SetBool("Move", false);
            anim.SetBool("Run", false);

            anim.SetBool("Move", true);
            state = State.Move;
		}

    }
    
    void MoveUpdate()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {

            // -1 : a or left  1 : d or right
            float horizontal = Input.GetAxis("Horizontal");
            // -1 : w or up  1 : s or down
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                speed = 6f;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            if(Input.GetButtonDown("Run"))
			{
                anim.SetBool("Idle", false);
                anim.SetBool("Move", false);
                anim.SetBool("Run", false);

                anim.SetBool("Run", true);
                state = State.Run;
            }
            
        }
        else
		{
            anim.SetBool("Idle", false);
            anim.SetBool("Move", false);
            anim.SetBool("Run", false);

            anim.SetBool("Idle", true);
            state = State.Idle;
        }

    }
    void RunUpdate()
	{
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && isRunDown)
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
                speed = 10f;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
        else
		{
            anim.SetBool("Idle", false);
            anim.SetBool("Move", false);
            anim.SetBool("Run", false);

            anim.SetBool("Move", true);
            state = State.Move;
		}
        
    }

    void GunUpdate()
    {
        // todo : aim 상태일때 카메라가 줌
        // todo : aim 상태일때 좌클릭하면 총 발사 - 레이캐스트
        if (isAimDown)
        {
            aimCamAnim.SetBool("ZoomIn", false);
            aimCamAnim.SetBool("ZoomOut", true);
        }
        else
        {
            aimCamAnim.SetBool("ZoomOut", false);
            aimCamAnim.SetBool("ZoomIn", true);
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
