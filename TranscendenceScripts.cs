using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscendenceScripts : Player2DController
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if ((Input.GetKeyDown(KeyCode.U) && isPlayer1) || (Input.GetKeyDown(KeyCode.Q) && !isPlayer1))
        {
            if (ammo != 0 && canShoot)
            {
                ammo--;
                {
                    ShootUp();
                }
            }
        }
        base.Update();
    }
    public override void AssignText()
    {
        base.AssignText();
    }

    public override void UpdateHPText()
    {
        base.UpdateHPText();
    }

    public override void UpdateAmmoText()
    {
        base.UpdateAmmoText();
    }

    public override void Move()
    {
        if ((Input.GetKey(KeyCode.UpArrow) && isPlayer1) || (Input.GetKey(KeyCode.F) && !isPlayer1))
        {
            facingForward = true;
            transform.Translate(0, moveSpeed / 1000, 0);
        }
        if ((Input.GetKey(KeyCode.DownArrow) && isPlayer1) || (Input.GetKey(KeyCode.V) && !isPlayer1))
        {
            facingForward = false;
            transform.Translate(0 ,-moveSpeed / 1000, 0);
        }
        if ((Input.GetKey(KeyCode.RightArrow) && isPlayer1) || (Input.GetKey(KeyCode.B) && !isPlayer1))
        {
            facingForward = true;
            transform.Rotate(new Vector3(0, 0, -6));
        }
        if ((Input.GetKey(KeyCode.LeftArrow) && isPlayer1) || (Input.GetKey(KeyCode.C) && !isPlayer1))
        {
            facingForward = false;
            transform.Rotate(new Vector3(0, 0, 6));
        }
    }

    void ShootUp()
    {
        asrce.PlayOneShot(shootAudio, 0.7F);
        GameObject proj = Instantiate(projectile, new Vector3(transform.position.x, transform.localPosition.y - 1.5f, transform.position.z), Quaternion.identity);
        proj.GetComponent<ProjectileScript>().player = this.gameObject;
        StartCoroutine(Cooldown());
    }

    public override void Recharge()
    {
        base.Recharge();
    }

    public override IEnumerator Cooldown()
    {
        return base.Cooldown();
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Shield()
    {
        if ((isPlayer1 && Input.GetKeyDown(KeyCode.O)) || (!isPlayer1 && Input.GetKeyDown(KeyCode.E)))
        {
                guard = Instantiate(shield, new Vector3(transform.position.x + shieldDistance, transform.position.y, transform.position.z), Quaternion.identity);
                guard.transform.parent = gameObject.transform;
                holdingShield = true;
                canShoot = false;
        }
        if ((isPlayer1 && Input.GetKeyUp(KeyCode.O)) || (!isPlayer1 && Input.GetKeyUp(KeyCode.E)))
        {
            Destroy(guard.gameObject);
            holdingShield = false;
            canShoot = true;
        }
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
    }

}
