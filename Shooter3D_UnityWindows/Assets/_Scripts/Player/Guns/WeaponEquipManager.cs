using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipManager : MonoBehaviour
{
    [SerializeField] private WeaponLoader _weaponLoader;
    [SerializeField] private GameObject _weapon;

    [SerializeField] private Animator _animator;
    [SerializeField] private AnimatorOverrideController _animatorPistol;

    private void Start()
    {
        LoadCurrentWeapon();
    }
    
    private void LoadCurrentWeapon()
    {
        _animator.runtimeAnimatorController = _animatorPistol;

        _weaponLoader.LoadWeapon(_weapon);
    }
    

    
}
