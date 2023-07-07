using Script.Helper;
using Script.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.ETC
{
    public class RePosition : MonoBehaviour
    {
        private Collider2D m_Col = null;

        private void Awake()
        {
            gameObject.GetComp(ref m_Col);
        }

        private const int TILE_SIZE = 40;
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag(ConstHelper.AREA_TAG))
                return;
            
            var _player = GameManager.Instance.Player;
            if (_player == null)
                return;

            var _playerPos = _player.transform.position;
            var _tilePos = transform.position;

            var _diffX = Mathf.Abs(_playerPos.x - _tilePos.x);
            var _diffY = Mathf.Abs(_playerPos.y - _tilePos.y);

            Vector3 _playerDir = _player.InputVec;
            var _dirX = _playerDir.x < 0 ? -1 : 1;
            var _dirY = _playerDir.y < 0 ? -1 : 1;
            
            switch (transform.tag)
            {
                case ConstHelper.GROUND_TAG:
                    switch (_diffX >= _diffY)
                    {
                        case true:
                            transform.Translate(Vector3.right * _dirX * TILE_SIZE);
                            break;
                        case false:
                            transform.Translate(Vector3.up * _dirY * TILE_SIZE);
                            break;
                    }
                    break;
                
                case ConstHelper.ENEMY_TAG:
                    if (m_Col.enabled)
                    {
                        transform.Translate(_playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                    }
                    break;
            }
        }
        
    }
}
