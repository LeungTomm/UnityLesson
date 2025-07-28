using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenTesting : MonoBehaviour
{

    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cube.transform.DOMove(new Vector3(10, 0, 0), 2f).SetEase(Ease.InOutQuad));
        sequence.Append(cube.transform.DOMove(new Vector3(10, 0, 10), 2f).SetEase(Ease.InOutQuad));
        sequence.Append(cube.transform.DOMove(new Vector3(10, 10, 10), 2f).SetEase(Ease.InOutQuad));
        sequence.Play();
        */

        Vector3[] path = { new Vector3(0, 0, 0), new Vector3(5, 5, 0), new Vector3(10, 0, 0) };
        transform.DOPath(path, 3f, PathType.CatmullRom);
        
        // cube.transform.DOMove(new Vector3(10, 0, 10), 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
