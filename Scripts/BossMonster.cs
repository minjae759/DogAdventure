using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

class BossMonsterStat : Character
{
    [SerializeField]
    private int maxhp;

    public int MAXHP{ get { return maxhp; } set { maxhp = value; } }
}

public class BossMonster : MonoBehaviour
{
    private BossMonsterStat bossMonsterStat = new BossMonsterStat();

    bool inRange;
    bool isThrow;
    bool isPunch;
    bool isDie;
    bool isAttack;

    float traceRange;
    float targetRadius;
    float targetRange;
    float attackSpeed;
    float delay;

    int rockPoolidx;

    Vector3 targetPos;

    private Transform target;
    private NavMeshAgent nav;
    private Animator animator;
    private Rigidbody rigidbody;
    private Material material;
    private Canvas canvas;
    [SerializeField]
    private BoxCollider meleeArea;
    [SerializeField]
    private Transform rightHandPos;
    [SerializeField]
    private Image hpbar;
    [SerializeField]
    private Text hptext;

    [SerializeField]
    private GameObject rockPrefab;
    private List<GameObject> rockPool;

    void Start()
    {
        traceRange = 5f;
        targetRange = 2f;
        targetRadius = 0.05f;
        attackSpeed = 4f;
        delay = 0f;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        material = GetComponentInChildren<Renderer>().material;
        rigidbody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        canvas = GetComponentInChildren<Canvas>();

        rockPool = new List<GameObject>();

        for (int i = 0; i < 5; i++)
        {
            GameObject rock = Instantiate(rockPrefab);
            rock.SetActive(false);
            rockPool.Add(rock);
        }
    }

    private void OnEnable()
    {
        bossMonsterStat.HP = 300;
        bossMonsterStat.MAXHP = bossMonsterStat.HP;
        bossMonsterStat.STR = 20;
        bossMonsterStat.DEF = 10;
    }

    void Update()
    {
        AdjustHpbar();
        Serch();
        Move();
        Attack(); 
        Die();
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void FreezeVelocity()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    void AdjustHpbar()
    {
        float curHP = bossMonsterStat.HP;
        float maxHP = bossMonsterStat.MAXHP;
        hpbar.fillAmount = (curHP / maxHP);
        hptext.text = bossMonsterStat.HP + " / " + bossMonsterStat.MAXHP;
    }

    void Serch()
    {
        targetPos = target.position;
        Vector3 thispos = gameObject.transform.position;

        inRange = Vector3.Distance(thispos, targetPos) < traceRange ? true : false;

        animator.SetBool("Walk", !inRange);
    }

    void Move()
    {
        if(nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = inRange;
        }
        else
        {
            nav.isStopped = true;
        }
    }

    void Die()
    {
        if (bossMonsterStat.HP <= 0 && !isDie)
        {
            isDie = true;
            gameObject.layer = 12;
            animator.SetTrigger("Die");
            SFXManager.instance.PlayStageClearclip();
            Invoke("Disable", 2.5f);
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameEnd();
        GameManager.instance.LoadScene("MainTitle");
    }

    void Attack()
    {
        delay += Time.deltaTime;
        if (attackSpeed < delay && inRange && !isAttack)
        {
            transform.LookAt(target);
            targetPos = target.position;
           
            isThrow = inRange;
            
            RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
            isPunch = raycastHits.Length > 0 ? true : false;

            if (isThrow && !isPunch && !isAttack) Throw();
            else if (isThrow && isPunch && !isAttack) Punch();
        }
    }

    void Punch()
    {
        isAttack = true;
        animator.SetBool("Punch", true);
        StopCoroutine("PunchMotion");
        StartCoroutine("PunchMotion");
    }

    void Throw()
    {
        isAttack = true;
        animator.SetBool("Throw", true);
        StopCoroutine("ThrowMotion");
        StartCoroutine("ThrowMotion");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && !isDie)
        {
            int attackerSTR = Player.instance.GetPlayerATK();
            int defenderDEF = bossMonsterStat.DEF;
            int damage = GameManager.instance.GetDamage(attackerSTR, defenderDEF);

            bossMonsterStat.HP -= damage;
            if (bossMonsterStat.HP <= 0) bossMonsterStat.HP = 0;

            UGUIManager.instance.DisplayText(canvas, damage.ToString());

            StopCoroutine("Hurt");
            StartCoroutine("Hurt");
        }
    }

    IEnumerator PunchMotion()
    {
        yield return new WaitForSeconds(0.7f);
        meleeArea.enabled = true;
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;
        delay = 0;
        animator.SetBool("Punch", false);
        isAttack = false;
        yield break;
    }

    IEnumerator ThrowMotion()
    {
        yield return new WaitForSeconds(1.6f);
        rockPool[rockPoolidx].GetComponent<Rock>().SetTarget(target.position);
        rockPool[rockPoolidx].transform.position = rightHandPos.position;
        rockPool[rockPoolidx++].SetActive(true);
        if (rockPoolidx >= rockPool.Count) rockPoolidx = 0;
        yield return new WaitForSeconds(1f);
        delay = 0;
        animator.SetBool("Throw", false);
        isAttack = false;
        yield break;
    }

    IEnumerator Hurt()
    {
        material.color = new Color(0.4716f, 0.4716f, 0.4716f);
        yield return new WaitForSeconds(0.2f);
        material.color = Color.white;
        yield break;
    }

    public int GetSTR()
    {
        return bossMonsterStat.STR;
    }
}