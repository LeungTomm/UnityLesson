using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour
{
    [Header("QTE Setting")]
    [SerializeField] private KeyCode qteKey = KeyCode.E;
    [SerializeField] private float qteWindow = 0.7f;

    [Header("Camera Shake Feeback")]
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float sucessAmplitude = 0.01f;
    [SerializeField] private float failureAmplitude = 0.2f;

    [Header("UI Feedback")]
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI qtePromptText;
    [SerializeField] private Image timerBarFill;

    private bool playerIsNear = false;
    private bool qteActive = false;
    private bool chestOpened = false;

    void Start()
    {
        interactText.gameObject.SetActive(false);
        qtePromptText.gameObject.SetActive(false);
        if (timerBarFill != null) timerBarFill.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.F) && !chestOpened && !qteActive)
        {
            StartCoroutine(StartQTE());
        }

        if (qteActive && Input.GetKeyDown(qteKey))
        {
            qteActive = false;
            StopAllCoroutines();
            OnQTESuccess();
        }
    }

    private IEnumerator StartQTE()
    {
        qteActive = true;

        interactText.DOFade(0, 0.2f);

        qtePromptText.text = $"PRESS {qteKey}!";
        qtePromptText.gameObject.SetActive(true);
        qtePromptText.transform.DOScale(1.2f, qteWindow / 2).SetLoops(2, LoopType.Yoyo);

        timerBarFill.transform.parent.gameObject.SetActive(true);
        timerBarFill.fillAmount = 1f;
        timerBarFill.DOFillAmount(0, qteWindow).SetEase(Ease.Linear);

        yield return new WaitForSeconds(qteWindow);

        if (qteActive)
        {
            qteActive = false;
            OnQTEFailure();
        }
    }

    private void OnQTESuccess()
    {
        Debug.Log("SUCESS! Chest Opened!");
        chestOpened = true;

        impulseSource.GenerateImpulseWithForce(sucessAmplitude);

        HideQTEUI();
    }

    private void OnQTEFailure()
    {
        Debug.Log("FAILURE! Try again!");

        impulseSource.GenerateImpulseWithForce(failureAmplitude);

        HideQTEUI();
    }

    private void HideQTEUI()
    {
        qtePromptText.DOKill();
        qtePromptText.gameObject.SetActive(false);

        timerBarFill.DOKill();
        timerBarFill.transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !chestOpened)
        {
            playerIsNear = true;
            interactText.gameObject.SetActive(true);
            interactText.DOFade(1, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            interactText.DOFade(0, 0.5f).OnComplete(() => interactText.gameObject.SetActive(false));
        }
    }
}
