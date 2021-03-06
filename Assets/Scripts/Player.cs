using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

    [SerializeField] float m_MoveSpeed;
    [SerializeField] float m_AccelerationTime;
    [SerializeField] float m_MaxJumpHeight;
    [SerializeField] float m_MinJumpHeight;
    [SerializeField] float m_TimeToJumpApex;
    [SerializeField] float m_CoyoteTime;

    float m_Gravity;
    float m_MaxJumpVelocity;
    float m_MinJumpVelocity;
    Vector3 m_Velocity;
    bool m_Jumped;
    float m_VelocityXSmoothing;
    float m_TargetVelocityX;
    float m_RemainingCoyoteTime;
    Animator m_Animator;

    Controller2D m_Controller;

    void Start() {
        // Calculate max/min jump velocity and gravity
        m_Gravity = -(2 * m_MaxJumpHeight) / Mathf.Pow(m_TimeToJumpApex, 2);
        m_MaxJumpVelocity = Mathf.Abs(m_Gravity) * m_TimeToJumpApex;
        m_MinJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(m_Gravity) * m_MinJumpHeight);

        m_Controller = GetComponent<Controller2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update() {
        // Update horizontal velocity
        m_Velocity.x = Mathf.SmoothDamp(m_Velocity.x, m_TargetVelocityX, ref m_VelocityXSmoothing, m_AccelerationTime);

        // Keep Y velocity 0 if on the ground or if touched the ceiling
        if ((m_Controller.m_Collisions.above || m_Controller.m_Collisions.below) && !m_Jumped) {
            m_Velocity.y = 0;
            m_Animator.SetBool("isJumping", false);
        }

        // Reset Coyote time if on the ground; otherwise, decrease it
        if (m_Controller.m_Collisions.below) {
            m_RemainingCoyoteTime = m_CoyoteTime;
        } else {
            m_RemainingCoyoteTime = Mathf.Max(0, m_RemainingCoyoteTime - Time.deltaTime);
        }

        // ...but set it to zero if the player just jumped
        if (m_Jumped) {
            m_RemainingCoyoteTime = 0;
            m_Jumped = false;
        }
        
        // Gravity
        m_Velocity.y += m_Gravity * Time.deltaTime;

        // Move
        m_Controller.Move(m_Velocity * Time.deltaTime);
        m_Animator.SetBool("isMoving", false);

        if (m_Velocity.x == 0 && m_Velocity.y == 0) {
            m_Animator.SetBool("isMoving", true);
        }
    }

    public void OnMove(InputValue value) {
        int dir = (int) value.Get<float>();
        m_TargetVelocityX = m_MoveSpeed * dir;

        // Face direction
        if (dir == 1) {
            // right
            Vector3 localScale = transform.localScale;
            localScale.x = 1f;
            transform.localScale = localScale;
        }
        else if (dir == -1) {
            // left
            Vector3 localScale = transform.localScale;
            localScale.x = -1f;
            transform.localScale = localScale;
        }
    }

    public void OnJump(InputValue value) {
        int jumped = (int) Mathf.Round(value.Get<float>());
        if (jumped == 1) {
            if (m_Controller.m_Collisions.below || m_RemainingCoyoteTime > 0) {
                m_Jumped = true;
                m_Velocity.y = m_MaxJumpVelocity;
                AudioManager._instance.Play("Jump");
                m_Animator.SetBool("isJumping", true);
            }
        } else {
            m_Velocity.y = Mathf.Min(m_Velocity.y, m_MinJumpVelocity);
        }
    }

    public void LevelOver() {
        Destroy(gameObject);
    }
}