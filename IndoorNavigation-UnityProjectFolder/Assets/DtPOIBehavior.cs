using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DtPOIBehavior : MonoBehaviour
{

    public Canvas cvs;   
    public Image whiteFadePanel;
    
    Vector3 originalScale;
    Vector3 minimizedScale;

    Color originalColor;
    Color minimizedColor = Color.white;

    private void Awake()
    {
        if (!cvs)
        {
            Debug.LogError("GameObject " + gameObject.name + " does not have CANVAS component!");
            return;
        }
        if (!whiteFadePanel)
        {
            Debug.LogError("whiteFadePanel parameter not assigned!");
            return;
        }

        //Store original scale
        originalScale = cvs.GetComponent<RectTransform>().localScale;
        minimizedScale = originalScale / 50;
        //Set to minimized scale
        cvs.GetComponent<RectTransform>().localScale = minimizedScale;

        //Store original color
        originalColor = whiteFadePanel.color;
        //Set WhiteFadePanel to all white
        whiteFadePanel.color = minimizedColor;

        //Disable the canvas gameobject
        cvs.gameObject.SetActive(false);

    }
    

    public void PopIn()
    {
        
        cvs.gameObject.SetActive(true);

        //Tween to original
        var sequence = DOTween.Sequence();
        Tween tween;
        tween = cvs.GetComponent<RectTransform>().DOScale(originalScale, .5f).SetEase(Ease.OutBack);
        sequence.Append(tween);
        tween = whiteFadePanel.DOColor(originalColor, .5f).SetEase(Ease.InCirc);
        sequence.Append(tween);

        //sequence.OnComplete(() => img.transform.rotation = Quaternion.identity);

    }

    public void PopOut()
    {
        //Tween to minimized
        var sequence = DOTween.Sequence();
        Tween tween;
        tween = whiteFadePanel.DOColor(minimizedColor, .4f).SetEase(Ease.InCirc);
        sequence.Append(tween);
        tween = cvs.GetComponent<RectTransform>().DOScale(minimizedScale, .2f).SetEase(Ease.OutBack);
        sequence.Append(tween);
        
    }
}
