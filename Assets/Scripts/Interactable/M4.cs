using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class M4 : Interactable
{
    //
    public GameObject m4;
    public Transform player, gunContainer, cam;
    //
    public Rigidbody rb;
    public bool equipped;
    Weapon weapon;

    void Start()
    {
        weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }

        if (equipped)
        {
            weapon.isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        if (weapon.readyToShoot && weapon.isShooting && weapon.bulletsLeft > 0)
        {
            weapon.FireWeapon();
        }

        if (equipped && Input.GetKeyDown(KeyCode.R) && weapon.bulletsLeft < weapon.magazineSize && weapon.isReloading == false)
        {
            AmmoManager.Instance.ammoDisplay.text = $"Reloading...";
            weapon.Reload();
        }

        if (weapon.readyToShoot && weapon.isShooting == false && weapon.isReloading == false && weapon.bulletsLeft == 0)
        {
            AmmoManager.Instance.ammoDisplay.text = $"Reloading...";
            weapon.Reload();
        }
    }

    protected override void Interact()
    {
        AmmoManager.Instance.ammoDisplay.text = $"{weapon.bulletsLeft}/{weapon.magazineSize}";
        if (!equipped && !slotFull) 
        { 
        Debug.Log("Interavted with " + gameObject.name);
        m4.transform.SetParent(gunContainer);
        m4.transform.localPosition = UnityEngine.Vector3.zero;
        m4.transform.localRotation = UnityEngine.Quaternion.Euler(UnityEngine.Vector3.zero);
        //
        equipped = true;
        slotFull = true;
        rb.isKinematic = true;
        }
    }

    private void Drop()
    {
        AmmoManager.Instance.ammoDisplay.text = $"0/0";
        equipped = false;
        slotFull = false;
        m4.transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(cam.forward, ForceMode.Impulse);
    }
}
