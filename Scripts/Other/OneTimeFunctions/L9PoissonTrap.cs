using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

public class L9PoissonTrap : MonoBehaviour
{
    public ParticleSystem poisson;
    public Animator playerA, levelTransitionAnimator;
    public TextMeshPro playerTMP;
    public GameObject rebornTransform, playerObject;
    public string[] playerLanguage;

    AudioManager audioManager;

    [Header("Active-Deactive")]
    public GameObject desertBG;
    public GameObject desertPS, nightBG, nightPS;
    public Color lightt;
    public Light2D a;


    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(PoissonTrap());
        }
    }

    IEnumerator PoissonTrap()
    {
        poisson.Play();
        yield return new WaitForSeconds(0.5f);
        playerTMP.text = playerLanguage[PlayerPrefs.GetInt("language")];
        playerTMP.GetComponent<TextMeshPro>().DOFade(1, 0.4f);
        PlayerController.isDead = true;
        yield return new WaitForSeconds(2.5f);
        playerA.Play("Die");
        audioManager.playSound("manDie1");
        yield return new WaitForSeconds(2f);
        levelTransitionAnimator.Play("slovlyFadeOn");
        //hava karadý 

        yield return new WaitForSeconds(3f);
        playerA.Play("Idle");
        audioManager.desertT = 0;
        audioManager.nightT = 46;
        playerObject.GetComponent<PlayerController>().speed = 6;
        playerObject.GetComponent<PlayerController>().jumpForce = 8.5f;
        playerTMP.text = null;
        playerTMP.GetComponent<TextMeshPro>().DOFade(0, 0f);
        rebornTransform.GetComponent<Transform>().DOMove(new Vector3(80, 2.6f), 0);
        playerObject.GetComponent<Transform>().DOMove(new Vector3(58, 2.6f), 0);
        desertBG.SetActive(false);
        desertPS.SetActive(false);
        nightBG.SetActive(true);
        nightPS.SetActive(true);
        PlayerController.isDead = false;
        levelTransitionAnimator.Play("slovlyFadeOff");
        a.color = lightt;

    }
}
