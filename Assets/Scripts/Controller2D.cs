using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2D Controller that handles collsions and movement.
[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour {
    
    public LayerMask m_CollisionMask;
    const float m_SkinWidth = 0.015f;
    public int m_HorizontalRayCount = 4;
    public int m_VerticalRayCount = 4;

    float m_HorizontalRaySpacing;
    float m_VerticalRaySpacing;

    BoxCollider2D m_Collider;
    RaycastOrigins m_RaycastOrigins;
    public CollisionInfo m_Collisions;

    void Start() {
        m_Collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity) {
        UpdateRaycastOrigins();
        m_Collisions.Reset();
        
        if (velocity.x != 0) {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.y != 0) {
            VerticalCollisions(ref velocity);
        }
        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector3 velocity) {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + m_SkinWidth;

         for (int i = 0; i < m_HorizontalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1) ? m_RaycastOrigins.bottomLeft : m_RaycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (m_HorizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, m_CollisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit) {
                velocity.x = (hit.distance - m_SkinWidth) * directionX;
                rayLength = hit.distance;

                m_Collisions.left = directionX == -1;
                m_Collisions.right = directionX == 1;
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity) {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + m_SkinWidth;

        for (int i = 0; i < m_VerticalRayCount; i++) {
            Vector2 rayOrigin = (directionY == -1) ? m_RaycastOrigins.bottomLeft : m_RaycastOrigins.topLeft;
            rayOrigin += Vector2.right * (m_VerticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, m_CollisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit) {
                velocity.y = (hit.distance - m_SkinWidth) * directionY;
                rayLength = hit.distance;

                m_Collisions.below = directionY == -1;
                m_Collisions.above = directionY == 1;
            }
        }
    }

    void UpdateRaycastOrigins() {
        Bounds bounds = m_Collider.bounds;
        bounds.Expand(m_SkinWidth * -2);

        m_RaycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        m_RaycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        m_RaycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        m_RaycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing() {
        Bounds bounds = m_Collider.bounds;
        bounds.Expand(m_SkinWidth * -2);

        m_HorizontalRayCount = Mathf.Clamp(m_HorizontalRayCount, 2, int.MaxValue);
        m_VerticalRayCount = Mathf.Clamp(m_VerticalRayCount, 2, int.MaxValue);

        m_HorizontalRaySpacing = bounds.size.y / (m_HorizontalRayCount - 1);
        m_VerticalRaySpacing = bounds.size.x / (m_VerticalRayCount - 1);
        
    }

    struct RaycastOrigins {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo {
        public bool above, below, left, right;
        public void Reset() {
            above = below = left = right = false;
        }
    }
}
