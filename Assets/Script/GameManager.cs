using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UI_Equip equip;
    public UI_Inventory inventory;
    public UI_SpellMaster spellM;
    public Image HpBar;
    public Image MpBar;
    public Image ExpBar;
    public DataBase DB;
    public float maxExp;
    public float maxHP;
    public float nowHP;
    public float maxMP;
    public float nowMP;
    public float Speed;

    public Image image;
    // public delegate void BB(string a, string b);
    //public event BB name;
    public bool isOpen;
    public bool onShield;
    private bool statusUpdate = true;

    public PostProcessVolume volume;
    private Vignette vignette;
    private ChromaticAberration chromatic;
    private void Awake()
    {
        //name += AXW;
        //name += AXWs;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        onShield = false;
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        isOpen = false;
        maxExp = DB.info.Level * 20;
        maxHP = 100 + (equip.HP * 100);
        maxMP = 100 + (equip.MP * 100);
        nowHP = maxHP;
        nowMP = maxMP;
        Speed = 5 + (equip.SPEED * 5);

        volume.profile.TryGetSettings(out vignette);
        volume.profile.TryGetSettings(out chromatic);
    }
    // Update is called once per frame
    void Update()
    {
        ExpBar.fillAmount = DB.info.Exp / maxExp;
        UpdateUI(); 
        if (isOpen)
        {
            nowMP -= Time.deltaTime*30f;
            if(nowMP<=0)
            {
                isOpen = false;
            }
        }
        if(DB.info.Exp >= maxExp)
        {
            DB.info.Level++;
            DB.info.Exp = 0;
            maxExp = DB.info.Level * 100;
        }
        if(nowMP<maxMP)
        {
            nowMP += Time.deltaTime*20f;
        }
        MpBar.fillAmount = nowMP / maxMP;
        HpBar.fillAmount = nowHP / maxHP;
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            nowHP = maxHP;
        }
        if(isOpen)
        {
            Time.timeScale = 0.75f;

            if (vignette.intensity.value <= 0.3f)
                vignette.intensity.value += 0.15f * Time.deltaTime;
            if (chromatic.intensity.value <= 1f)
                chromatic.intensity.value += 0.5f * Time.deltaTime;
        }
        else
        {
            vignette.intensity.value = 0f;
            chromatic.intensity.value = 0f;
            Time.timeScale = 1f;
        }
    }

    public void UpdateUI()
    {
        Speed = 5 + (equip.SPEED * 5);
        maxHP = 100 + (equip.HP * 100);
        if (nowHP > maxHP) nowHP = maxHP;
        maxMP = 100 + (equip.MP * 100);
        if (nowMP > maxMP) nowMP = maxMP;
        maxExp = DB.info.Level * 100;
        equip.UpdateItem();
        inventory.UpdateItem();
        //spellM.UpdateUI();

    }

    public void OnDamage(float damage)
    {
        if(!onShield)
        nowHP -= damage;
    }

    public IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    }

    public IEnumerator FadeInCoroutine()
    {
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    }

}