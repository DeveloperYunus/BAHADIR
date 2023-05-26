using UnityEngine;
using TMPro;
using DG.Tweening;

public  class SkillManaCost : MonoBehaviour
{
    public TextMeshProUGUI sword, rage, fire, ice, regen, protect;
    public GameObject closeTalkImage;
    
    float zaman;
    PlayerController pc;

    [Header("PEffect")]
    public GameObject protectParticle;
    public GameObject regenParticle, rageParticle, soilParticle;

    void Start()
    {
        pc = GetComponent<PlayerController>();
        WriteManaCost();
    }
    private void FixedUpdate()
    {
        if (Time.time >= zaman)
        {
            zaman = Time.time + 1;
            if (!PlayerController.isTalking)
            {
                closeTalkImage.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            }
            else
            {
                closeTalkImage.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            }
        }
    }
    public void WriteManaCost()
    {
        sword.text = pc.EB.lastSword.energyPrice.ToString();
        rage.text = null;

        fire.text = pc.EB.lastSword.energyPrice.ToString();
        ice.text = (2f * pc.EB.lastSword.energyPrice).ToString();
        regen.text = pc.skillEnergy[3].ToString();
        protect.text = pc.skillEnergy[2].ToString();
    }
}
