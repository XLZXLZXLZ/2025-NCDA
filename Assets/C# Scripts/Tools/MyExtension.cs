using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MyExtensions
{
    public static class DOTweenExtension
    {
        public static Tween DOStartWidth(this LineRenderer lineRenderer, float endValue, float duration, Ease ease = Ease.Linear)
        {
            return DOTween.To(
                () => lineRenderer.startWidth,               // ��ȡ��ǰֵ
                x => lineRenderer.startWidth = x,              // ������ֵ
                endValue,                                      // Ŀ��ֵ
                duration)                                      // ����ʱ��
                .SetEase(ease)                                 // ���û�������
                .SetTarget(lineRenderer);                      // ����Ŀ�������ڿ���
        }
    }
}
