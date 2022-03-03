using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

class MonsterStat : Character
{
    [SerializeField]
    private int maxhp;

    public int MAXHP { get { return maxhp; } set { maxhp = value; } }
}

public class Monster : MonoBehaviour
{
    bool inRange;
    bool isTrace;
    bool isBattle;
    bool isAttack;
    bool isDie;

    float delay;
    float traceRange;
    float attackspeed;

    Vector3 targetPos;
    MonsterStat monsterStat = new MonsterStat();

    private Transform target;
    private NavMeshAgent nav;
    private Animator animator;
    private Rigidbody rigidbody;
    private Material material;
    [SerializeField]
    private Image hpbar;
    private Text hptext;
    [SerializeField]
    private Canvas canvas;
    public BoxCollider meleeArea;

    private void Awake()
    {
        isTrace = true;
        delay = 0f;
        traceRange = 4f;
        attackspeed = 4f;

        targetPos = Vector3.zero;

        target = GameObject.FindWithTag("Player").transform;
        material = GetComponentInChildren<Renderer>().material;
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        hptext = GetComponentInChildren<Text>();
        canvas = GetComponentInChildren<Canvas>();
    }

    private void OnEnable()
    {
        StatInit();
        meleeArea.enabled = false;
        isTrace = true;
        isAttack = false;
        isDie = false;
        material.color = Color.white;
        delay = 0f;
        gameObject.layer = 11;
    }

    void Update()
    {
        Search();
        Move();
        Attack();
        AdjustHpbar();
        Die();
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }

    void StatInit()
    {
        monsterStat.HP = 50;
        monsterStat.MAXHP = monsterStat.HP;
        monsterStat.STR = 10;
        monsterStat.DEF = 1;
    }

    void FreezeVelocity()
    {
        if (inRange)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    void Search()
    {
        targetPos = target.position;
        Vector3 thispos = gameObject.transform.position;

        inRange = Vector3.Distance(thispos, targetPos) < traceRange ? true : false;

        animator.SetBool("Walk", inRange);
    }

    void Move()
    {
        if (nav.enabled && inRange && !isDie)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isTrace;
        }
        else
        {
            nav.isStopped = true;
        }
    }

    void Attack()
    {
        delay += Time.deltaTime;

        float targetRadius = 0.05f;
        float targetRange = 1f;
        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        
        isTrace = raycastHits.Length > 0 ? false : true;
        isBattle = raycastHits.Length > 0 ? true : false;
        animator.SetBool("BattleStance", isBattle);

        if (!isAttack && isBattle && !isDie && delay > attackspeed)
        {
            transform.LookAt(target);
            StartCoroutine("BodyBlow");
        }
    }

    void AdjustHpbar()
    {
        float curHP = monsterStat.HP;
        float maxHP = monsterStat.MAXHP;
        hpbar.fillAmount = (curHP / maxHP);
        hptext.text = monsterStat.HP + " / " + monsterStat.MAXHP;
    }

    void Die()
    {
        if(monsterStat.HP <= 0 && !isDie)
        {
            GameManager.instance.SetDeadMonster();
            isDie = true; 
            gameObject.layer = 12;
            animator.SetTrigger("Die");
            Invoke("Disable", 2.5f);
            // 확률적으로 아이템 드랍        
            ItemManager.instance.DropPotion(transform.position);
            Player.instance.GainEXP(7);
        }
    }

    private void Disable() 
    {
        gameObject.SetActive(false);
    }

    IEnumerator BodyBlow()
    {
        isAttack = true;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.3f);

        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.7f);

        meleeArea.enabled = false;
        isTrace = true;
        isAttack = false;

        animator.SetBool("Attack", false);
        delay = 0f;

        yield break;
    }

    IEnumerator Hurt()
    {
        material.color = new Color(0.4716f, 0.4716f, 0.4716f);
        yield return new WaitForSeconds(0.2f);
        material.color = Color.white;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && !isDie)
        {
            int attackerSTR = Player.instance.GetPlayerATK();
            int defenderDEF = monsterStat.DEF;
            int damage = GameManager.instance.GetDamage(attackerSTR, defenderDEF);

            monsterStat.HP -= damage;
            if (monsterStat.HP <= 0) monsterStat.HP = 0;

            UGUIManager.instance.DisplayText(canvas, damage.ToString());

            StopCoroutine("Hurt");
            StartCoroutine("Hurt");
        }
    }

    public int GetSTR()
    {
        return monsterStat.STR;
    }
}