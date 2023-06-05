using System.Collections;
using UnityEngine;
public class ZombieAttack : MonoBehaviour
{
        [SerializeField] private float _attackRange;
        [SerializeField] private int _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _timeToDamage;
        public AudioClip musicClip;
        private AudioSource musicSource;
        private float _timer;
        public bool CanAttack { get; private set; }
        private Player _player;
        public float AttackRange => _attackRange;

        private void Start()
        {
                _player = FindObjectOfType<Player>();
        }

        private void Update()
        {
                if (CanAttack)
                {
                        return;
                }
                _timer += Time.deltaTime;
                if (_timer < _cooldown)
                {
                        return;
                }
                CanAttack = true;
                _timer = 0;
        }

        public void TryAttackPlayer()
        {
                if (!musicSource.isPlaying)
                {
                        musicSource.clip = musicClip;
                        musicSource.Play();
                }
                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <
                    _attackRange)
                {
                        _player.TakeDamage(_damage);
                        CanAttack = false;
                }
        }

}