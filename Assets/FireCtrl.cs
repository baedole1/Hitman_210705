using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{

    public GameObject bullet;
    public Transform firePos;
    public ParticleSystem muzzleFlash;
    //public AudioClip fireSfx; // ����

    //private AudioSource _audio; // ������ ���


    void Start()
    {
        //_audio = GetComponent<AudioSource>(); // �����ϸ� ��� �����Ͷ� 
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

        RaycastHit hit;


        if (Input.GetMouseButtonDown(0))
        {
            Fire();

            if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f))
            {
                if (hit.collider.CompareTag("ENEMY"))
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    hit.collider.GetComponent<EnemyCtrl>().OnDamage(25.0f);
                }
            }

        }
    }
    void Fire()
    {
        muzzleFlash.Play();
        //Instantiate(bullet, firePos.position, firePos.rotation);
        

    }
}