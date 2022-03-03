using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

/*
 * DMG 계산방식
 * DMG = Attacker.STR - Target.DEF;
*/

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField]
    private Transform playerbody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private float jumpforce = 250.0f;
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private Material material;
    [SerializeField]
    private List<GameObject> equipWeapons;
    private int curjump;

    float time = 0f;
    float originDrag;
    float waterDrag;
    int weaponidx;

    int atk; // 플레이어STR과 무기 DMG합산 공격력

    bool isDefend;
    bool isWall;
    bool isHurt;
    bool isDead;

    Rigidbody rigidbody;
    Animator animator;
    Weapon equipweapon;

    public ParticleSystem levelUpEffect;


    void Start()
    {
        if (instance == null)
            instance = this;

        curjump = 0;

        isDefend = false;
        isHurt = false;
        isDead = false;
        originDrag = 0;
        weaponidx = 0;
        waterDrag = 5;
        material.color = Color.white;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        SetPlayerATK();
    }

    void Update()
    {
        if (!isDead)
        {
            AdjustHpbar();
            AdjustEXPbar();
            Move();
            ChangingWeapon();
            Attack();
            Defend();
        }
        LookAround();
        Die();
    }

   /* void SaveData()
    {
        
    }*/

    private void LookAround()
    {
        if (!GameManager.instance.GetIsPause())
        {
            Vector2 mousedelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 camaerAngle = cameraArm.rotation.eulerAngles;
            float x = camaerAngle.x - mousedelta.y;

            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }

            cameraArm.rotation = Quaternion.Euler(x, camaerAngle.y + mousedelta.x, camaerAngle.z);
        }
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;

        animator.SetBool("Run", isMove);

        if (isMove && !isDefend)
        {
            Vector3 lookFoward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookright = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookFoward * moveInput.y + lookright * moveInput.x;

            //캐릭터가 항상 카메라가 바라보는 방향으로 봄
            //characterBody.forward = lookFoward;

            //캐릭터의 방향과 카메라의 방향이 서로다름
            playerbody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * speed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && PlayerStatManager.instance.GetMaxJump() > curjump)
        {
            SFXManager.instance.PlayJumpclip();
            curjump++;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector3.up * jumpforce);
        }
    }

    private void Attack()
    {
        bool isleftClick = Input.GetMouseButtonDown(0);
        time += Time.deltaTime;

        if (isleftClick && time > equipweapon.attackSpeed && !isHurt && !isDefend)
        {
            Targeting();
            equipweapon.Use();
            animator.SetTrigger("Attack");
            SFXManager.instance.PlaySwingclip();
            time = 0f;
        }
    }

    private void Targeting()
    {
        Vector3 targetPos = TargetingArea.instance.GetPosition();
        if (targetPos != Vector3.zero)
        {
            targetPos = targetPos - transform.position;
            targetPos.y = 0f;
            playerbody.rotation = Quaternion.LookRotation(targetPos);
        }
    }

    private void Defend()
    {
        isDefend = Input.GetMouseButton(1);
        animator.SetBool("Defend", isDefend);
    }

    void AdjustHpbar()
    {
        float curHP = PlayerStatManager.instance.GetHp();
        float maxHP = PlayerStatManager.instance.GetMaxHp();
        string levelText = "Lv." + PlayerStatManager.instance.GetLevel();
        UGUIManager.instance.AdjacentHPbar(curHP / maxHP);
        UGUIManager.instance.AdjacentHPbarText(curHP + " / " + maxHP);
        UGUIManager.instance.AdjacentLevelText(levelText);
    }

    void AdjustEXPbar()
    {
        float curEXP = PlayerStatManager.instance.GetExp();
        float maxEXP = PlayerStatManager.instance.GetMaxExp();
        float expPercentage = curEXP / maxEXP * 100f;
        string expText = string.Format("{0:F3}", expPercentage) + "% [ " + curEXP + " / " + maxEXP + " ]";
        UGUIManager.instance.AdjacentEXPbar(curEXP / maxEXP);
        UGUIManager.instance.AdjacentEXPbarText(expText);
    }

    public void GainEXP(int exp)
    {
        int curEXP = PlayerStatManager.instance.GetExp();
        curEXP += exp;
        PlayerStatManager.instance.SetExp(curEXP);
        if (PlayerStatManager.instance.GetMaxExp() <= curEXP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        int curEXP = PlayerStatManager.instance.GetExp();
        int curLevel = PlayerStatManager.instance.GetLevel();

        if (curLevel < PlayerStatManager.instance.GetMaxLevel())
        {
            // 초과 경험치 보존
            curLevel++;

            int statpoint = StatPanelDataManager.instance.GetStatPoint();
            statpoint += 5;
            StatPanelDataManager.instance.SetStatPoint(statpoint);

            curEXP -= PlayerStatManager.instance.GetMaxExp();
            PlayerStatManager.instance.SetExp(curEXP);
            PlayerStatManager.instance.SetLevel(curLevel);
            PlayerStatManager.instance.SetMaxExp(curLevel - 1);
            levelUpEffect.Play();
            SFXManager.instance.PlayPlayerLevelUpclip();
        }
        else
        {
            curEXP = PlayerStatManager.instance.GetMaxExp();
            PlayerStatManager.instance.SetExp(curEXP);
        }
    }

    private void Die()
    {
        if (PlayerStatManager.instance.GetHp() <= 0 && !isDead)
        {
            isDead = true;
            GameManager.instance.GameEnd();
            animator.SetTrigger("Die");
        }
    }

    private void ChangingWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !equipweapon.isSwing)
        {
            int preidx = weaponidx;
            int nextidx = weaponidx + 1;

            while (preidx != nextidx)
            {
                if (nextidx >= equipWeapons.Count) nextidx = 0;
                if (PlayerStatManager.instance.GetIsHavingWeapon(nextidx) == true) break;
                nextidx++;
            }

            if (PlayerStatManager.instance.GetIsHavingWeapon(nextidx) == true)
            {
                equipWeapons[preidx].SetActive(false);
                equipWeapons[nextidx].SetActive(true);
                weaponidx = nextidx;
                SetPlayerATK();
            }
        }
        equipweapon = equipWeapons[weaponidx].GetComponent<Weapon>();
    }

    public void SetPlayerATK()
    {
        atk = PlayerStatManager.instance.GetSTR() + equipWeapons[weaponidx].GetComponent<Weapon>().GetWeaponDamage();
    }

    public int GetPlayerATK()
    {
        return atk;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap" && !isHurt && !isDefend && !isDead)
        {
            int hp = PlayerStatManager.instance.GetHp();
            hp -= PlayerStatManager.instance.GetMaxHp() / 10;
            if (hp <= 0) hp = 0;
            PlayerStatManager.instance.SetHp(hp);
            StartCoroutine("Hurt");
        }
        else if (other.tag == "MonsterAttackArea" && !isHurt && !isDefend && !isDead)
        {
            int attackerSTR;
            if (other.gameObject.GetComponentInParent<Monster>() != null)
            {
                //일반 몬스터일경우
                attackerSTR = other.gameObject.GetComponentInParent<Monster>().GetSTR();
            }
            else
            {
                //보스 몬스터일경우
                attackerSTR = other.gameObject.GetComponentInParent<BossMonster>().GetSTR();
            }

            int defenderDEF = PlayerStatManager.instance.GetDEF();
            int damage = GameManager.instance.GetDamage(attackerSTR, defenderDEF);
            int hp = PlayerStatManager.instance.GetHp();

            hp -= damage;
            if (hp <= 0) hp = 0;
            PlayerStatManager.instance.SetHp(hp);

            StartCoroutine("Hurt");
        }
        else if (other.tag == "Weapon" && !isDead)
        {
            for (int i = 0; i < equipWeapons.Count; i++)
            {
                string itemName = other.name;
                if (itemName.Contains(equipWeapons[i].name))
                {
                    SFXManager.instance.PlayGetItemclip();
                    PlayerStatManager.instance.SetIsHavingWeapon(i,true);
                }
            }
        }
        else if (other.tag == "Potion" && !isDead)
        {
            SFXManager.instance.PlayGetItemclip();
            if (other.name.Contains("small"))
            {
                int hp = PlayerStatManager.instance.GetHp();
                hp += 5;
                if (hp >= PlayerStatManager.instance.GetMaxHp()) hp = PlayerStatManager.instance.GetMaxHp();
                PlayerStatManager.instance.SetHp(hp);
            }
            else if (other.name.Contains("medium"))
            {
                int hp = PlayerStatManager.instance.GetHp();
                hp += 20;
                if (hp >= PlayerStatManager.instance.GetMaxHp()) hp = PlayerStatManager.instance.GetMaxHp();
                PlayerStatManager.instance.SetHp(hp);
            }
            else if (other.name.Contains("large"))
            {
                int hp = PlayerStatManager.instance.GetHp();
                hp += 50;
                if (hp >= PlayerStatManager.instance.GetMaxHp()) hp = PlayerStatManager.instance.GetMaxHp();
                PlayerStatManager.instance.SetHp(hp);
            }
        }
        else if (other.name == "EndPortal" && !isDead)
        {
            PlayerStatManager.instance.SaveData();
            StatPanelDataManager.instance.SaveData();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Water")
        {
            curjump = 0;
            rigidbody.drag = waterDrag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            rigidbody.drag = originDrag;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // 움직이는 발판일때 발판을 부모로 두어 발판의위치를 따라감
            transform.parent = collision.transform;
            curjump = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            transform.parent = null;
        }
    }

    IEnumerator Hurt()
    {
        isHurt = true;
        for (int i = 0; i < 3; i++)
        {
            material.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            material.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }
        isHurt = false;
        yield break;
    }
}