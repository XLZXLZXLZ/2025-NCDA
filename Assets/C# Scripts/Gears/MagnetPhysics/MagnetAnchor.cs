using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MagnetAnchor : MonoBehaviour
{
    [Header("Anchor Settings")]
    public Vector2 localOffset = Vector2.zero; // ����ڸ������λ��ƫ��
    public float debugRadius = 0.1f;           // ������ʾ�뾶

    public MagnetBody parentBody;             // �����Ĵ���

    private void Awake()
    {
        parentBody = GetComponentInParent<MagnetBody>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    public Vector2 WorldPosition =>
        (Vector2)parentBody.transform.TransformPoint(localOffset);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, debugRadius);
    }
}