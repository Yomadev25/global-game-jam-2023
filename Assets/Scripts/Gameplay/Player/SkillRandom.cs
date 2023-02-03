using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillRandom : MonoBehaviour
{
    [SerializeField] private GameObject _skill1UI;
    [SerializeField] private GameObject _skill2UI;

    int skillRandom1 = 0;
    int skillRandom2 = 0;
    int skillSelect = 0;

    private void Start()
    {
        StartCoroutine(RandomSkill());
    }

    IEnumerator RandomSkill()
    {
        int index = Random.Range(0, SkillManager.instance.GetSkillIndexLength());

        while (true)
        {
            index = Random.Range(0, SkillManager.instance.GetSkillIndexLength());

            if (!SkillManager.instance.GetSkillIndexCheck(index))
            {
                skillRandom1 = index;
                break;
            }
            yield return null;
        }
        var skill1Image = _skill1UI.GetComponent<Image>();
        //Set sprite
        var skill1Button = _skill1UI.GetComponent<Button>();
        skill1Button.onClick.AddListener(() =>
        {
            SelectSkill(index);
        });
        
        while (true)
        {
            index = Random.Range(0, SkillManager.instance.GetSkillIndexLength());

            if (!SkillManager.instance.GetSkillIndexCheck(index))
            {
                skillRandom2 = index;
                break;
            }
            yield return null;
        }
        var skill2Image = _skill2UI.GetComponent<Image>();
        //Set sprite
        var skill2Button = _skill2UI.GetComponent<Button>();
        skill2Button.onClick.AddListener(() =>
        {
            SelectSkill(index);
        });
    }

    public void SelectSkill(int index)
    {
        skillSelect = index;
        //Deactive Canvas
        //Game Start
    }

    public void UnlockSkill()
    {
        SkillManager.instance.GetSkillData().SkillUnlocks[skillSelect] = true;
        //Scene Change
    }
}
