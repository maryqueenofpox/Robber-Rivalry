using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    //public float moveSpeed { get { return _moveSpeed; } protected set { _moveSpeed = value; }   } something fancy
    public float moveSpeed { get { return _moveSpeed; } }
    public float dashForce { get { return _dashForce; } }
    public float reach { get { return _reach; } }
    public float slapFoce { get { return _slapFoce; } }
    public float slapCooldown { get { return _slapCooldown; } set { } }
    public float dashDuration { get { return _dashDuration; } set { } }
    public float bashForce { get { return _bashForce; } }
    public float bulletVelocity { get { return _bulletVelocity; } }
    public float gemPointIncrease { get { return _gemPointIncrease; } }
    public float timeUntilScoreIncrease { get { return _timeUntilScoreIncrease; } set { } }
    public float stunDuration { get { return _stunDuration; } set { } }
    public float timeToSetSlapToFalse { get { return _timeToSetSlapToFalse; } set { } }
    public float durationToIncreaseBy { get { return _durationToIncreaseBy; } }
    public float vulnerableTimer { get { return _vulnerableTimer; } set { } }

    [Header("Movement Values")]
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _dashForce = 10f;

    [Header("Abilities")]
    [SerializeField] GameObject _wetFloorSign;
    [SerializeField] float _reach = 1.5f;
    [SerializeField] float _slapFoce = 5000f;
    [SerializeField] float _slapCooldown = 3f;
    [SerializeField] float _dashDuration = 2f;
    [SerializeField] float _bashForce = 2000000f;

    [SerializeField] GameObject _rayGun;
    [SerializeField] GameObject _rayBullet;
    [SerializeField] float _bulletVelocity = 50f;

    [Header("Loot Grabber Script")]
    [SerializeField] LootGrabber _lootGrabber;

    [Header("Gem Score")]
    [SerializeField] float _gemPointIncrease;
    [SerializeField] float _timeUntilScoreIncrease = 4.0f;

    [SerializeField] float _stunDuration = 1f;

    [SerializeField] float _timeToSetSlapToFalse = 1f;
    [SerializeField] float _durationToIncreaseBy = 0.1f;

    [SerializeField] AudioSource _powerUpAudio;

    [SerializeField] float _vulnerableTimer = 1f;
}
