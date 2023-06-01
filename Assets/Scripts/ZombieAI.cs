using UnityEngine;
using Pathfinding;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private float _minWalkableDistance;
    [SerializeField] private float _maxWalkableDistance;
    [SerializeField] private float _reachedPointDistance;
    [SerializeField] private GameObject _roamTarget;
    [SerializeField] private GameObject _soundTarget;
    [SerializeField] private float _targetFollowRange;
    [SerializeField] private float _stopTargetFollowingRange;
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private AIPath _aiPath;
    [SerializeField] private float _microphoneDistance;
    public AudioSource source;
    private Animator _animator;

    public float threshold = 0.1f;
    public float loudnessSensibility = 100;

    private Player _player;
    private EnemyStates _currentState;
    private Vector3 _roamPosition;
    private Vector3 _soundPosition;

    void TryFindTarget()
    {
        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <= _targetFollowRange)
        {
            _currentState = EnemyStates.Following;
        }
    }

    void TryHearTarget()
    {
        float loudness = MicrophoneRecorder.volumeLevel * loudnessSensibility;
        if (loudness < threshold) loudness = 0;
        if (loudness > _microphoneDistance)
        {
            _soundPosition = _player.transform.position;
            _soundTarget.transform.position = _soundPosition;
            _currentState = EnemyStates.MovingToSound;
        }
    }

    Vector3 GenerateRoamPosition()
    {
        Vector3 randomDirection = GenerateRandomDirection();
        Vector3 targetPosition = transform.position + randomDirection * GenerateRandomWalkableDistance();

        // Проверить доступность точки перемещения на сетке Grid Graph
        NNConstraint constraint = NNConstraint.Default;
        constraint.constrainWalkability = true;
        constraint.walkable = true;
        GraphNode node = AstarPath.active.GetNearest(targetPosition, constraint).node;

        if (node != null && !node.Walkable)
        {
            // Если точка недоступна, выполнить повторную генерацию
            return GenerateRoamPosition();
        }

        return (Vector3)node.position;
    }

    float GenerateRandomWalkableDistance()
    {
        var randomDistance = Random.Range(_minWalkableDistance, _maxWalkableDistance);
        return randomDistance;
    }

    Vector3 GenerateRandomDirection()
    {
        var newDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        return newDirection.normalized;
    }

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetBool("IsWalking", true);
        _player = FindObjectOfType<Player>();
        _currentState = EnemyStates.Roaming;
        _roamPosition = GenerateRoamPosition();
    }

    void Update()
    {
        switch (_currentState)
        {
            case EnemyStates.Roaming:
                _animator.SetBool("IsWalking", true);
                _animator.SetBool("IsFollowing", false);
                _aiPath.maxSpeed = 1.5f;
                _roamTarget.transform.position = _roamPosition;
                if (Vector3.Distance(gameObject.transform.position, _roamPosition) <= _reachedPointDistance)
                {
                    _roamPosition = GenerateRoamPosition();
                }

                _aiDestinationSetter.target = _roamTarget.transform;
                TryFindTarget();
                TryHearTarget();
                break;

            case EnemyStates.Following:
                _animator.SetBool("IsFollowing", true);
                _animator.SetBool("IsWalking", false);
                _aiPath.maxSpeed = 3;
                _aiDestinationSetter.target = _player.transform;
                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <
                    _enemyAttack.AttackRange)
                {
                    if (_enemyAttack.CanAttack)
                    {
                        _enemyAttack.TryAttackPlayer();
                        _animator.SetTrigger("Attack");
                    }
                }
                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) >=
                    _targetFollowRange)
                {
                    _roamPosition = GenerateRoamPosition();
                    _currentState = EnemyStates.Roaming;
                }
                break;

            case EnemyStates.MovingToSound:
                _animator.SetBool("IsFollowing", true);
                _animator.SetBool("IsWalking", false);
                _aiPath.maxSpeed = 3;
                TryHearTarget();
                _aiDestinationSetter.target = _soundTarget.transform;

                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _targetFollowRange)
                {
                    _currentState = EnemyStates.Following;
                }

                if (Vector3.Distance(gameObject.transform.position, _soundTarget.transform.position) < 3)
                {
                    _currentState = EnemyStates.Roaming;
                }
                break;
        }
    }

    public enum EnemyStates
    {
        Roaming,
        Following,
        MovingToSound
    }
}