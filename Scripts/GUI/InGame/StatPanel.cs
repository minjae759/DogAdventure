using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



public class StatPanel : MonoBehaviour
{

    public Text hp_Text;
    public Text str_Text;
    public Text def_Text;
    public Text statpoint_Text;

    public Slider hp_Slider;
    public Slider str_Slider;
    public Slider def_Slider;

    int hp_idx;
    int str_idx;
    int def_idx;
    int statpoint;

    float[] percentage = { 0f, 0.207f, 0.414f, 0.621f, 0.828f, 1f };

    private void OnEnable()
    {
        hp_idx = StatPanelDataManager.instance.GetHpidx();
        str_idx = StatPanelDataManager.instance.GetSTRidx();
        def_idx = StatPanelDataManager.instance.GetDEFidx();
        statpoint = StatPanelDataManager.instance.GetStatPoint();

        hp_Slider.value = percentage[hp_idx];
        str_Slider.value = percentage[str_idx];
        def_Slider.value = percentage[def_idx];
        SetHpText();
        SetStrText();
        SetDEFText();
        SetStatpointText();
    }

    private void OnDisable()
    {
        Player.instance.SetPlayerATK();
        gameObject.SetActive(false);
    }

    public void SetHpText()
    {
        hp_Text.text = PlayerStatManager.instance.GetMaxHp().ToString();
    }

    public void SetStrText()
    {
        str_Text.text = PlayerStatManager.instance.GetSTR().ToString();
    }

    public void SetDEFText()
    {
        def_Text.text = PlayerStatManager.instance.GetDEF().ToString();
    }

    public void SetStatpointText()
    {
        statpoint_Text.text = statpoint.ToString();    
    }

    public void PlusHpSliderValue()
    {
        if(hp_idx < 5 && statpoint > 0)
        {
            hp_idx++;
            statpoint--;
            PlayerStatManager.instance.SetMaxHp(hp_idx);
            hp_Slider.value = percentage[hp_idx];
            SetHpText();
            SetStatpointText();
            StatPanelDataManager.instance.SetHpidx(hp_idx);
            StatPanelDataManager.instance.SetStatPoint(statpoint);
        }
    }

    public void PlusSTRSliderValue()
    {
        if (str_idx < 5 && statpoint > 0)
        {
            str_idx++;
            statpoint--;
            PlayerStatManager.instance.SetSTR(str_idx);
            str_Slider.value = percentage[str_idx];
            SetStrText();
            SetStatpointText();
            StatPanelDataManager.instance.SetSTRidx(str_idx);
            StatPanelDataManager.instance.SetStatPoint(statpoint);
        }
    }

    public void PlusDEFSliderValue()
    {
        if (def_idx < 5 && statpoint > 0)
        {
            def_idx++;
            statpoint--;
            PlayerStatManager.instance.SetDEF(def_idx);
            def_Slider.value = percentage[def_idx];
            SetDEFText();
            SetStatpointText();
            StatPanelDataManager.instance.SetDEFidx(def_idx);
            StatPanelDataManager.instance.SetStatPoint(statpoint);
        }
    }

    public void MinusHpSliderValue()
    {
        if (hp_idx > 0)
        {
            hp_idx--;
            statpoint++;
            PlayerStatManager.instance.SetMaxHp(hp_idx);
            if(PlayerStatManager.instance.GetHp() > PlayerStatManager.instance.GetMaxHp())
            {
                PlayerStatManager.instance.SetHp(PlayerStatManager.instance.GetMaxHp());
            }
            hp_Slider.value = percentage[hp_idx];
            SetHpText();
            SetStatpointText();
            StatPanelDataManager.instance.SetHpidx(hp_idx);
            StatPanelDataManager.instance.SetStatPoint(statpoint);
        }
    }

    public void MinusSTRSliderValue()
    {
        if (str_idx > 0)
        {
            str_idx--;
            statpoint++;
            PlayerStatManager.instance.SetSTR(str_idx);
            str_Slider.value = percentage[str_idx];
            SetStrText();
            SetStatpointText();
            StatPanelDataManager.instance.SetSTRidx(str_idx);
            StatPanelDataManager.instance.SetStatPoint(statpoint);
        }
    }

    public void MinusDEFSliderValue()
    {
        if (def_idx > 0)
        {
            def_idx--;
            statpoint++;
            PlayerStatManager.instance.SetDEF(def_idx);
            def_Slider.value = percentage[def_idx];
            SetDEFText();
            SetStatpointText();
            StatPanelDataManager.instance.SetDEFidx(def_idx);
            StatPanelDataManager.instance.SetStatPoint(statpoint);
        }
    }
}