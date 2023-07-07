using System;
using UnityEngine;

namespace Script.ETC
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private float m_ScanRange = 0f;
        [SerializeField] private LayerMask m_TargetMask;
        [SerializeField] private RaycastHit2D[] m_Target;
        [SerializeField] private Transform m_NearestTarget;
        public Transform NearestTarget => m_NearestTarget;

        private void FixedUpdate()
        {
            m_Target = Physics2D.CircleCastAll(transform.position, m_ScanRange, Vector2.zero, 0f, m_TargetMask);
            m_NearestTarget = GetNearest();
        }

        private Transform GetNearest()
        {
            Transform _result = null;
            var _diff = 100f;
            var _myPos = transform.position;
            
            foreach (var _hit2D in m_Target)
            {
                var _targetPos = _hit2D.transform.position;
                var _curDiff = Vector3.Distance(_myPos, _targetPos);
                if (_curDiff < _diff)
                {
                    _diff = _curDiff;
                    _result = _hit2D.transform;
                }
            }
            
            return _result;
        }
    }
}
