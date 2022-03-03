using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;   //무기데미지
    public float attackSpeed;   //공격속도
    float waitTime;
    public bool isSwing;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    private void OnEnable()
    {
        waitTime = (attackSpeed - 0.1f) / 2f;
        meleeArea.enabled = false;
        trailEffect.enabled = false;
    }

    public void Use()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    public int GetWeaponDamage()
    {
        return damage;
    }

    IEnumerator Swing()
    {
        isSwing = true;
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        yield return new WaitForSeconds(waitTime);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(waitTime);
        trailEffect.enabled = false;
        isSwing = false;
        yield break;
    }
}