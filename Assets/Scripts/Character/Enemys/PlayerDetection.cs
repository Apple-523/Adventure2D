
using UnityEngine;
/// <summary>
/// 用于侦查一定范围内是否有Player
/// </summary>
public class PlayerDetection : MonoBehaviour
{
    [Header("是否靠近Player")]
    [SerializeField]
    private bool isCloseToPlayer;

    [Header("侦查距离")]
    public float detectDistance;
    [Header("侦查中心点")]
    public Vector2 detectOffset;
    [Header("侦查Layer")]
    public LayerMask detectMask;
    private PhysicsCheckEventHandler eventHandler;

    private void Awake()
    {
        isCloseToPlayer = false;
        eventHandler = PhysicsCheckEventHandler.Instance;
    }

    private void Update()
    {
        Vector2 offset = detectOffset;
        offset.x *= transform.localScale.x;
        Vector2 startPosition = (Vector2)transform.position + offset;
        Vector2 endPosition = startPosition;
        endPosition.x = endPosition.x + transform.localScale.x * detectDistance * -1;
        bool isInDistance = Physics2D.Linecast(startPosition, endPosition, detectMask);
        
        if (isInDistance != isCloseToPlayer)
        {
            eventHandler.PlayerIsClose(isInDistance);
        }
        isCloseToPlayer = isInDistance;
    }


    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        // 画出中心点
        Gizmos.color = Color.cyan;
        Vector2 offset = detectOffset;
        offset.x *= transform.localScale.x;
        Vector2 startPosition = (Vector2)transform.position + offset;
        Gizmos.DrawWireSphere(startPosition, 0.1f);

        Vector2 endPosition = startPosition;
        endPosition.x = endPosition.x + transform.localScale.x * detectDistance * -1;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPosition, endPosition);
    }
}
