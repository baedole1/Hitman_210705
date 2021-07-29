using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{

    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    public State state = State.IDLE;
    private NavMeshAgent nv;
    private Animator anim;
    private Transform[] wayPoints;

    private Transform playerTr;
    public float traceDist = 6.0f;
    public float attackDist = 3.0f;

    private bool isDie = false;
    private WaitForSeconds ws3 = new WaitForSeconds(0.3f);

    private readonly int hashIsWalk = Animator.StringToHash("isWalk");
    private readonly int hashFire = Animator.StringToHash("Fire");
    private readonly int hashDie = Animator.StringToHash("Die"); //추가
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx"); //추가


    private bool isFire = false;
    public float fireRate = 0.2f;
    private float nextFire = 0.0f;

    public GameObject bullet;
    [System.NonSerialized]
    public Transform firePos;

    private float hp = 100.0f; // 추가


    void Awake()
    {
        nv = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        wayPoints = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>(); // 루트에 있는거찾아오는파인드
        firePos = transform.Find("Rweaponholder/FirePos").GetComponent<Transform>(); // 차일드에 있는거 가져오기
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();

    }

    void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());

    }

    // Use this for initialization
    void Start()
    {

        nv = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        wayPoints = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>(); // 루트에 있는거찾아오는파인드
        firePos = transform.Find("Rweaponholder/FirePos").GetComponent<Transform>(); // 차일드에 있는거 가져오기
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();

        int idx = Random.Range(1, wayPoints.Length);
        nv.SetDestination(wayPoints[idx].position);


    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            yield return ws3;

            if (state == State.DIE) yield break; // 새로추가됨


            float dist = Vector3.Distance(playerTr.position, transform.position);

            if (dist <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (dist <= traceDist)

            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }

    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    anim.SetBool(hashIsWalk, true);
                    nv.isStopped = false;
                    isFire = false;
                    break;

                case State.TRACE:
                    nv.SetDestination(playerTr.position);
                    nv.isStopped = false;
                    anim.SetBool(hashIsWalk, true);
                    isFire = false;
                    break;

                case State.ATTACK:
                    anim.SetBool(hashIsWalk, false);
                    anim.SetTrigger(hashFire);
                    nv.isStopped = true;
                    isFire = true;

                    break;

                case State.DIE:

                    this.gameObject.tag = "Untagged"; // 자신이 죽으면 태그를언태그로만듬
                    isDie = true; // 새로 추가됨
                    nv.isStopped = true; //새로 추가됨
                    GetComponent<CapsuleCollider>().enabled = false;
                    anim.SetFloat(hashDieIdx, Random.Range(0, 4)); //추가됨

                    anim.SetTrigger(hashDie); // 추가
                    nextFire = Time.time + fireRate;
                    yield return 3;
                    break;
            }

            if (state == State.ATTACK)
            {
                Vector3 dir = playerTr.position - transform.position;
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10.0f); // 선형보관  수는 속도
            }
        }

        void OnCollisionEnter(Collision coll)
        {
            if (coll.collider.CompareTag("BULLET")) ;
            {
                Destroy(coll.gameObject);
                hp -= 25.0f;
                if (hp <= 0.0f)
                {
                    state = State.DIE;
                }
            }
        }

        void OnPlayerDie()
        {

            //GmaeObject.Find("GameManager").GetComponent<GameManager>().isGameOver = true;

            isFire = false;
            isDie = true;
            StopAllCoroutines();
            anim.SetTrigger("PlayerDie");
            nv.isStopped = true;
        }

        
    }
    public void OnDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0.0f)
        {
            state = State.DIE;
        }
    }
}