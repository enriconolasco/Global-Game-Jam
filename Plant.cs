using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Player2DController
{
    bool canShootSpecial = true;
    public GameObject specialProjectile;

    bool canFart = true;

    public AudioClip specialAudio;
    public AudioClip potatoAudio;

    public void Reset()
    {
        moveSpeed = 120f;
        jumpHeight = 500f;
        ammo = 8;
        numberOfJumps = 3;
        maxAmmo = 8;
        shootForce = 400f;
        hp = 1;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void UpdateHPText()
    {
        base.UpdateHPText();
    }

    public override void UpdateAmmoText()
    {
        base.UpdateAmmoText();
    }

    public override void Update()
    {
        Die();
        if ((Input.GetKeyDown(KeyCode.U) && isPlayer1) || (Input.GetKeyDown(KeyCode.Q) && !isPlayer1))
        {
            if (ammo != 0 && canShoot)
            {
                ammo--;
                {
                    if(Input.GetKey(KeyCode.UpArrow) && isPlayer1 || (Input.GetKey(KeyCode.F) && !isPlayer1))
                    {
                        ShootUp();
                    }
                    else if (!(Input.GetKey(KeyCode.UpArrow)) && isPlayer1 || !((Input.GetKey(KeyCode.F))&& !isPlayer1))
                    {
                        Shoot();
                    }
                }
            }
        }
        base.Update();
    }

    void ShootUp()
    {
        asrce.PlayOneShot(shootAudio, 0.7F);
        GameObject proj = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * shootForce);
            StartCoroutine(Cooldown());
    }

    public override IEnumerator Cooldown()
    {
        return base.Cooldown();
    }

    public override void Fart()
    {
        if ((isPlayer1 && Input.GetKeyDown(KeyCode.J)) && canFart|| (!isPlayer1 && Input.GetKeyDown(KeyCode.A)) && canFart)
        {
            asrce.PlayOneShot(potatoAudio, 0.7F);
            fartInstance = Instantiate(fart, new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z), Quaternion.identity);
            fartInstance.GetComponent<plantFart>().player = this.gameObject;
            StartCoroutine(FartCooldown());
        }
    }

    public override IEnumerator FartCooldown()
    {
        canFart = false;
        yield return new WaitForSeconds(5);
        canFart = true;
    }

    new void Die()
    {
        if(hp <= 0)
        {
            if (!canShootSpecial)
            {
                specialProjectile.GetComponent<plantSpecial>().SpawnPlant();
                Instantiate(letterF, new Vector3(0, 0, 0), Quaternion.identity);
                Destroy(this.gameObject);
            }
            else
            {
                Instantiate(letterF, new Vector3(0, 0, 0), Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    public override void AssignText()
    {
        base.AssignText();
    }

    public override void Shield()
    {
        base.Shield();
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public override void Recharge()
    {
        base.Recharge();
    }

    public override void SpecialMove()
    {
        if ((Input.GetKeyDown(KeyCode.K) && isPlayer1) || (Input.GetKeyDown(KeyCode.S) && !isPlayer1))
        {
            if (canShootSpecial && facingForward)
            {
                asrce.PlayOneShot(specialAudio, 0.7F);
                GameObject proj = Instantiate(specialProjectile, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Quaternion.identity);
                canShootSpecial = false;
            }
        }
        if ((Input.GetKeyDown(KeyCode.K) && isPlayer1) || (Input.GetKeyDown(KeyCode.S) && !isPlayer1))
        {
            if (canShootSpecial && !facingForward)
            {
                asrce.PlayOneShot(specialAudio, 0.7F);
                GameObject proj = Instantiate(specialProjectile, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), Quaternion.identity);
                canShootSpecial = false;
            }
        }
    }

    public override void Jump()
    {
        base.Jump();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            numberOfJumps = 3;
        }
    }
}
