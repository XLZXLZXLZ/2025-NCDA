using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagnetBody : MonoBehaviour
{
    public Vector2 Velocity => rb.velocity;

    public Vector2 magnetMultiplier = new Vector2(1,1); //ˮƽ�������ֱ����Դ�������Ӧ�̶�

    private MagnetAnchor[] anchors;    // ����ê��
    private Rigidbody2D rb;           // �������

    protected virtual void Awake()
    {
        anchors = GetComponentsInChildren<MagnetAnchor>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Ӧ������������
    public void ApplyCombinedForce(Vector2 totalForce)
    {
        var x = totalForce.x * magnetMultiplier.x;
        var y = totalForce.y * magnetMultiplier.y;

        rb.AddForce(new Vector2(x,y));
    }
}

