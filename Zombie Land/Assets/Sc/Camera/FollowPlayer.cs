using System;
using UnityEngine;

namespace Sc.Camera
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Transform _player;
        private Vector2 _ = Vector2.zero;
        private Vector2 _pos;
        
        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = _player.position;
        }

        private void Update()
        {
            _pos = Vector2.SmoothDamp(transform.position, _player.position, ref _, speed);
            transform.position = new Vector3(_pos.x, _pos.y, -10f);
        }
    }
}
