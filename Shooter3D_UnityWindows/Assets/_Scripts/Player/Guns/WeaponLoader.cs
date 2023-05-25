using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : MonoBehaviour
{

    private GameObject _weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadWeapon(GameObject weaponItem)
    {
        GameObject currentWeapon = Instantiate(weaponItem, transform);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.rotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one;

        _weapon = currentWeapon;
    }
}
