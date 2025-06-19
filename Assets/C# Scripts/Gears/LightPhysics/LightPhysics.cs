using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightInterAction
{
    Stop,
    Pencentrate,
    Reflect
}
public abstract class LightPhysics : MonoBehaviour
{
    public LightInterAction interaction;

    protected List<LightLine> lightLines = new();

    protected bool IsHit => lightLines.Count > 0;

    public void LightHit(LightLine incomingLight)
    {
        lightLines.Add(incomingLight);

        if (lightLines.Count == 1)
        {
            OnHit();
        }
    }

    protected abstract void OnHit();

    public void LightLeave(LightLine leavingLight)
    {
        if (!lightLines.Contains(leavingLight))
        {
            Debug.LogError("�쳣:����" + leavingLight.gameObject.name + " ���뿪����" + gameObject.name + "ʱ����δ�Ƚ������б�");
        }
        lightLines.Remove(leavingLight);
        if(lightLines.Count <= 0)
            OnLeave();
    }

    protected abstract void OnLeave();
}
