using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̶���ؽ�
public class InGameFixedJoint : InGameJoint
{
    private Color showColor = Color.green;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = showColor;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
