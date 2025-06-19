using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class RotateBar : Gear
{
    public enum TransformType
    {
        Loop,
        PingPong
    }

    public Vector3[] rotateStates;
    public TransformType transformType;

    [SerializeField]
    private int currentIndex;
    [SerializeField]
    private float moveTime = 1f;

    private int direction = 1; // 1��ʾ����-1��ʾ����

    protected override void Awake()
    {
        base.Awake();
        // ��ʼ��ʱ���õ���ǰ������Ӧ�ĽǶ�
        if (rotateStates != null && rotateStates.Length > 0)
        {
            transform.eulerAngles = rotateStates[currentIndex];
        }
    }

    private void Rotate()
    {
        if (rotateStates == null || rotateStates.Length == 0) return;

        // ���µ�ǰ����
        if (transformType == TransformType.Loop)
        {
            currentIndex = (currentIndex + 1) % rotateStates.Length;
        }
        else // PingPongģʽ
        {
            currentIndex += direction;
            // �߽��Ⲣ��ת����
            if (currentIndex >= rotateStates.Length || currentIndex < 0)
            {
                direction *= -1;
                currentIndex += 2 * direction;
            }
        }

        // ʹ��DOTween��ת��Ŀ��Ƕ�
        transform.DORotate(rotateStates[currentIndex], moveTime).SetEase(Ease.InQuad);
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();
        Rotate();
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();
        Rotate();
    }
}