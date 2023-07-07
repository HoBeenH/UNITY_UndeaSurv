using Script.Actor.Player;
using Script.Helper;
using Script.Manager;
using Script.Table;
using Script.Table.Model;
using UnityEngine;

namespace Script.ETC
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int m_ID;

        // TODO Cha Level Up Dif Value
        private float m_Damage;
        private int m_Cnt;
        private float m_Speed;
        private float m_Timer;

        private WeaponTableModel m_CurWeaponModel = null;
        private Player m_Player = null;
        private const int INFINITY_PER = -1;

        private void Awake()
        {
            m_Player = GetComponentInParent<Player>();
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            var _table = TableManager.Instance.Table.GetTable<WeaponTable>();
            D.Assert(_table != null, $"The Table Is NULL {nameof(WeaponTable)}");
            if (_table == null)
                return;

            m_CurWeaponModel = _table.GetData(m_ID);
            D.Assert(m_CurWeaponModel != null, $"The TableModel Is NULL {nameof(WeaponTableModel)} {m_ID.ToString()}");
            if (m_CurWeaponModel == null)
                return;

            m_Damage = m_CurWeaponModel.Damage;
            m_Cnt = m_CurWeaponModel.Count;
            m_Speed = m_CurWeaponModel.Speed;
            m_Timer = m_CurWeaponModel.Timer;

            if (m_CurWeaponModel.GetPoolType() is PoolManager.EPoolType.Sword)
                Spawn();
        }

        public void LevelUp(float damage, int cnt)
        {
            m_Damage = damage;
            m_Cnt += cnt;

            if (m_ID == 0)
                Spawn();
        }

        private void Fire()
        {
            if (m_Player.Scanner.NearestTarget == null)
                return;

            var _targetPos = m_Player.Scanner.NearestTarget.position;
            var _palyerPos = transform.position;
            var _dir = (_targetPos - _palyerPos).normalized;

            var _go = PoolManager.Instance.GetObject<Bullet>(m_CurWeaponModel.GetPoolType());
            _go.transform.SetPositionAndRotation(_palyerPos, Quaternion.FromToRotation(Vector3.up, _dir));
            _go.Init(m_Damage, m_Cnt, _dir);
        }

        private void Update()
        {
            if (m_CurWeaponModel == null)
                return;
            
            switch (m_CurWeaponModel.GetPoolType())
            {
                case PoolManager.EPoolType.Sword:
                    transform.Rotate(Vector3.forward * (m_Speed * Time.deltaTime));
                    break;
                case PoolManager.EPoolType.Bullet:
                    m_Timer += Time.deltaTime;
                    if (m_Timer > m_Speed)
                    {
                        m_Timer = 0f;
                        Fire();
                    }
                    break;
            }
        }

        private void Spawn()
        {
            for (var i = 0; i < m_Cnt; i++)
            {
                Sword _sword = PoolManager.Instance.GetObject<Sword>(PoolManager.EPoolType.Sword);
                if (_sword == null)
                    return;

                var _tr = _sword.transform;

                if (_tr.parent != this.transform)
                    _tr.parent = transform;
                
                _tr.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                
                var _rotVec = Vector3.forward * 360 * i / m_Cnt;
                _tr.Rotate(_rotVec);
                _tr.Translate(_tr.up * 1.5f, Space.World);
                _sword.Init(m_Damage, INFINITY_PER);
            }
        }
    }
}
