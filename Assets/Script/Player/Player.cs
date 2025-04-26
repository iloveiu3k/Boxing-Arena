using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerFocus))]
public class Player : Singleton<Player>
{
    private PlayerMovement _playerMovement;
    public PlayerMovement PlayerMovement { get => _playerMovement; }

    private PlayerAnimation _playerAnimation;
    public PlayerAnimation PlayerAnimation { get => _playerAnimation; }

    private PlayerAttack _playerAttack;
    public PlayerAttack PlayerAttack { get => _playerAttack; }

    private PlayerFocus _playerFocus;
    public PlayerFocus PlayerFocus { get => _playerFocus; }

    private PlayerStats _playerStats;
    public PlayerStats PlayerStats { get => _playerStats; }

    private PlayerEventAnimation _playerEventAnimation;
    public PlayerEventAnimation PlayerEventAnimation { get => _playerEventAnimation; }

    [SerializeField] PlayerDataSO _playerDataSO;
    public PlayerDataSO PlayerDataSO { get => _playerDataSO; }

    protected override void Awake()
    {
        base.Awake();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerFocus = GetComponent<PlayerFocus>(); 
        _playerEventAnimation = GetComponentInChildren<PlayerEventAnimation>();
        _playerStats = GetComponent<PlayerStats>();
    }

}
