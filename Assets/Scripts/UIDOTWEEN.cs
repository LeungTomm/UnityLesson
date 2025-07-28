using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIDOTWEEN : MonoBehaviour
{
    public GameObject cube;
    public RectTransform button;
    public CanvasGroup canvasGroup;

    public void Shrink()
    {
        button.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f).OnComplete(() => button.transform.DOScale(Vector3.one, 0.1f));
    }

    public void FadeIn()
    {
        canvasGroup.DOFade(1f, 0.5f);
    }

    public void FadeOut()
    {
        canvasGroup.DOFade(0f, 0.5f);
    }

}
