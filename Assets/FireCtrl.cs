using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{

    public GameObject bullet;
    public Transform firePos;
    public ParticleSystem muzzleFlash;
    //public AudioClip fireSfx; // 선언

    //private AudioSource _audio; // 가져올 목록


    void Start()
    {
        //_audio = GetComponent<AudioSource>(); // 시작하면 목록 가져와라 
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