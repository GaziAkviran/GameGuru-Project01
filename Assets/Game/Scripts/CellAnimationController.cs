using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class CellAnimationController : MonoBehaviour
{
    [SerializeField, BoxGroup("Text Animation Setup")] private float textAppearDuration = 0.2f;
    [SerializeField, BoxGroup("Text Animation Setup")] private float textDisappearDuration = 0.1f;

    [SerializeField, BoxGroup("Cell Animation Setup")] private float colorChangeDuration = 0.2f;
    [SerializeField, BoxGroup("Cell Animation Setup")] private Color cellDeselectColor;
    [SerializeField, BoxGroup("Cell Animation Setup")] private Color cellSelectColor;

    [SerializeField, BoxGroup("Cell Popup Animation Setup")] private float selectScaleMultiplier = 1.1f;
    [SerializeField, BoxGroup("Cell Popup Animation Setup")] private float selectScaleDuration = 0.15f;
    
    [SerializeField, BoxGroup("Cell Popup Animation Setup")] private float deselectScaleMultiplier = 0.9f;
    [SerializeField, BoxGroup("Cell Popup Animation Setup")] private float deselectScaleDuration = 0.1f;
    
    [SerializeField, Foldout("References")] private Transform textTransform;
    [SerializeField, Foldout("References")] private SpriteRenderer cellSpriteRenderer;
    [SerializeField, Foldout("References")] Transform visualTransform;
    [SerializeField, Foldout("References")] private ParticleSystem explosionParticle;
    
    private Tween textSelectTween, cellColorTween, cellSelectTween;

    public void Init()
    {
        if (cellSpriteRenderer != null)
        {
            cellSpriteRenderer.material = new Material(cellSpriteRenderer.material);
        }
        
        PlayCellDeselectAnimation(true);
        PlayTextHideAnimation(true);
    }
    
    [Button()]
    private void PlayTextShowAnimation(bool isInstant = false)
    {
        if (textTransform == null)
        {
            Debug.LogError("TextTransform is null. Please assign a transform");
            return;
        }

        if (isInstant)
        {
            textTransform.localScale = Vector3.one;
        }
        else
        {
            textSelectTween?.Kill();

            textSelectTween = textTransform.DOScale(Vector3.one, textAppearDuration)
                .SetEase(Ease.OutBack);
        }
        
    }

    [Button()]
    private void PlayTextHideAnimation(bool isInstant = false)
    {
        if (textTransform == null)
        {
            Debug.LogError("TextTransform is null. Please assign a Transform");
            return;
        }
        
        if (isInstant)
        {
            textTransform.localScale = Vector3.zero;
        }
        else
        {
            textSelectTween?.Kill();

            textSelectTween = textTransform.DOScale(Vector3.zero, textDisappearDuration)
                .SetEase(Ease.InBack);
        }
    }

    [Button()]
    public void PlayCellSelectAnimation(bool isInstant = false)
    {
        if (cellSpriteRenderer == null)
        {
            Debug.LogError("CellSpriteRenderer is null. Please assign a SpriteRenderer.");
            return;
        }

        if (isInstant)
        {
            cellSpriteRenderer.color = cellSelectColor;
        }
        else
        {
            cellColorTween?.Kill();
        
            PlayCellSelectPopupAnimation();
            PlayTextShowAnimation();
            cellColorTween = cellSpriteRenderer.DOColor(cellSelectColor, colorChangeDuration)
                .SetEase(Ease.OutQuad);
        }
        
    }

    [Button()]
    public void PlayCellDeselectAnimation(bool isInstant = false)
    {
        if (cellSpriteRenderer == null)
        {
            Debug.LogError("CellSpriteRenderer is null. Please assign a SpriteRenderer.");
            return;
        }

        if (isInstant)
        {
            cellSpriteRenderer.color = cellDeselectColor;
        }
        else
        {
            cellColorTween?.Kill();
        
            PlayCellDeselectPopupAnimation();
            PlayTextHideAnimation();
            cellColorTween = cellSpriteRenderer.DOColor(cellDeselectColor, colorChangeDuration)
                .SetEase(Ease.OutQuad);
        }
    }

    
    private void PlayCellSelectPopupAnimation()
    {
        if (visualTransform == null)
        {
            Debug.LogError("Visual is null. Please assign a Transform");
            return;
        }
        
        cellSelectTween?.Kill();
        
        cellSelectTween = visualTransform.DOScale(Vector3.one * selectScaleMultiplier, selectScaleDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => visualTransform.DOScale(Vector3.one, selectScaleDuration).SetEase(Ease.InQuad));
    }

    private void PlayCellDeselectPopupAnimation()
    {
        if (visualTransform == null)
        {
            Debug.LogError("Visual is null. Please assign a Transform");
            return;
        }
        
        cellSelectTween?.Kill();
        
        cellSelectTween = visualTransform.DOScale(Vector3.one * deselectScaleMultiplier, deselectScaleDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => visualTransform.DOScale(Vector3.one, deselectScaleDuration).SetEase(Ease.InSine));
    }
    
    [Button()]
    public void PlayBlastAnimation()
    {
        if (visualTransform == null)
        {
            Debug.LogError("Visual is null. Please assign a Transform");
            return;
        }
        
        visualTransform.DOShakeScale(0.3f, 0.2f)
            .OnComplete(() =>
            {
                visualTransform.DOScale(Vector3.one * 1.2f, 0.15f).SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        PlayExplosionEffect();
                        visualTransform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InQuad)
                            .OnComplete(() =>
                            {
                                //onComplete?.Invoke();
                                ResetCell();
                            });
                    });
            });
    }
    
    private void PlayExplosionEffect()
    {
        if (explosionParticle != null)
        {
            explosionParticle.Play();
        }
    }
    
    private void ResetCell()
    {
        visualTransform.localScale = Vector3.one;
    }
}
