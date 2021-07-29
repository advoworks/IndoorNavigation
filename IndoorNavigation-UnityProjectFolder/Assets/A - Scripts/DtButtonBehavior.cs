using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DtButtonBehavior : MonoBehaviour
{

    Button btn;
    Image img;
    private void Awake()
    {
        btn = GetComponent<Button>();
        if (!btn)
        {
            Debug.LogError("GameObject " + gameObject.name + " does not have an BUTTON component!");
        }

        img = GetComponent<Image>();
        if (!img)
        {
            Debug.LogError("GameObject " + gameObject.name + " does not have an IMAGE component!");
        }
    }
    private void OnEnable()
    {
        btn.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        btn.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        //Debug.Log("Onclick running DT");

        //var sequence = DOTween.Sequence();
        //Tween tween = img.transform.DORotate(endValue: transform.forward * -30f, duration: 0.5f)
        //    .SetLoops(2, LoopType.Yoyo);
        //sequence.Append(tween);
        //sequence.OnComplete(() => img.transform.rotation = Quaternion.identity);

        img.transform.DOPunchScale(punch: new Vector3(1, 1, 1), duration: 0.3f, vibrato: 3, elasticity: 1);
    }
}
