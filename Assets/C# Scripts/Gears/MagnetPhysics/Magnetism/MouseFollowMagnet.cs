using System;
using UnityEngine;

public class MouseFollowMagnet : MonoBehaviour
{
    [Header("Magnet Settings")]
    public float radius = 5f;
    public float minForce = 1f;
    public float maxForce = 100f;
    public float forceFactor = 100f;
    public LayerMask anchorLayer;
    public Color gizmoColor = Color.green;

    [SerializeField] float dampingFactor = 5f;    // ����ϵ��
    [SerializeField] float maxDampingForce = 50f;// ���������

    private Rigidbody2D rb;

    private bool isApplying = false;
    public bool IsApplying
    {
        get { return isApplying; }
        set
        {
            if (isApplying != value)
            {
                changeApplyState(value);
                AudioManager.Instance.PlaySe(value ? "ManetOpen" : "MagnetClose");
            }
            isApplying = value;
        }
    }

    public Action<bool> changeApplyState;


    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (!GameManager.Instance.CanInteractNow)
            return;

        if (Input.GetMouseButton(0))
        {
            IsApplying = true;
        }
        else
        {
            IsApplying = false;
        }
    }

    private void FixedUpdate()
    {
        FollowPointer();
        if(isApplying)
        {
            ApplyMagneticForce();
        }
    }

    void ApplyMagneticForce()
    {
        Collider2D[] anchors = Physics2D.OverlapCircleAll(
            transform.position,
            radius,
            anchorLayer
        );

        foreach (Collider2D anchorCollider in anchors)
        {
            MagnetAnchor anchor = anchorCollider.GetComponent<MagnetAnchor>();
            if (!anchor) continue;

            // ���㵥��ê�������
            Vector2 anchorPos = anchor.WorldPosition;
            Vector2 direction = (Vector2)transform.position - anchorPos;
            float distance = direction.magnitude;

            if (distance <= radius && distance > 0.001f)
            {
                // �����������
                float forceMagnitude = Mathf.Clamp(
                    forceFactor / Mathf.Pow(distance, 2),
                    minForce,
                    maxForce
                );
                Vector2 magneticForce = direction.normalized * forceMagnitude;

                // ���������������������ٶȷ����෴��
                Vector2 dampingForce = CalculateDampingForce(anchor.parentBody);

                // �ϲ�����Ӧ��
                anchor.parentBody.ApplyCombinedForce(magneticForce + dampingForce);
            }
        }
    }

    Vector2 CalculateDampingForce(MagnetBody body)
    {
        // ��ȡ�����ٶ�
        Vector2 velocity = body.Velocity;

        // �������������������ٶ��෴��
        Vector2 dampingDirection = -velocity.normalized;
        float dampingMagnitude = Mathf.Min(
            dampingFactor * velocity.magnitude,
            maxDampingForce
        );

        return dampingDirection * dampingMagnitude;
    }

    void FollowPointer() => rb.MovePosition(Tools.MousePosition);

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}