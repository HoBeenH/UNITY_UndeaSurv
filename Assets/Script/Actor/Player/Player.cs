using System;
using Script.ETC;
using Script.Helper;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Actor.Player
{
    public class Player : MonoBehaviour, PlayerInputAction.IPlayerActions
    {
        [Header("# PlayerMove Parameter")]
        [SerializeField] private float m_Speed = 0f;
        
        [Header("# ReadOnly")]
        [SerializeField] private Vector2 m_InputVec = Vector2.zero;

        public Scanner Scanner => m_Scanner;

        private unsafe void* _pInputVec => UnsafeUtility.AddressOf(ref m_InputVec);

        public Vector2 InputVec => m_InputVec;

        private PlayerInputAction m_Controller = null;
        private SpriteRenderer m_Sp = null;
        private Rigidbody2D m_Rb = null;
        private Animator m_At = null;
        private Scanner m_Scanner = null;

        private readonly int r_SpeedHash = Animator.StringToHash("Speed");
        private readonly int r_DeadHash = Animator.StringToHash("Dead");
        
        private void Awake()
        {
            gameObject.GetComp(ref m_Rb);
            gameObject.GetComp(ref m_Sp);
            gameObject.GetComp(ref m_At);
            gameObject.GetComp(ref m_Scanner);
            m_Controller = new PlayerInputAction();
            m_Controller.Player.SetCallbacks(this);
            m_Controller.Player.Enable();
        }

        private void FixedUpdate()
        {
            if (!CheckMoveCondition())
                return;

            var _nextVal = m_InputVec.normalized * (m_Speed * Time.fixedDeltaTime);
            m_Rb.MovePosition(m_Rb.position + _nextVal);
        }

        private void LateUpdate()
        {
            m_At.SetFloat(r_SpeedHash, m_InputVec.magnitude);
            if (m_InputVec.x != 0)
            {
                m_Sp.flipX = m_InputVec.x < 0;
            }
        }

        private bool CheckMoveCondition()
        {
            if (m_Rb == null)
                return false;

            return true;
        }

        public unsafe void OnMove(InputAction.CallbackContext context)
        {
            var _dir = context.ReadValue<Vector2>();
            if (_dir != Vector2.zero)
            {
                
            }
            
            context.ReadValue(_pInputVec, UnsafeUtility.SizeOf<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnFire(InputAction.CallbackContext context)
        {
        }
    }
}
