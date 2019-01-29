using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clifford : Player2DController
{
    public bool canShootSpecial = true;
    public GameObject specialProjectile;

    GameObject instance;
    GameObject instance2;
    GameObject instance3;

    public AudioClip fartSound;
    public AudioClip shootSound;
    public AudioClip specialSound;

    public void Reset()
    {
        shieldDistance = 1.5f;
        numberOfJumps = 1;
        shootForce = 150f;
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
        base.Update();
    }

    public override void Fart()
    {
        if ((isPlayer1 && Input.GetKeyDown(KeyCode.J)) && canUseFart || (!isPlayer1 && Input.GetKeyDown(KeyCode.A)) && canUseFart)
        {
            asrce.PlayOneShot(fartSound, 0.7F);
            fartInstance = Instantiate(fart, new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), Quaternion.identity);
            fartInstance.transform.parent = gameObject.transform;
            fartInstance.GetComponent<Fart>().player = this.gameObject;
            StartCoroutine(FartCooldown());
        }
        if ((isPlayer1 && Input.GetKeyUp(KeyCode.J)) || (!isPlayer1 && Input.GetKeyUp(KeyCode.A)))
        {
            Destroy(fartInstance.gameObject);
        }
    }

    public override IEnumerator FartCooldown()
    {
        return base.FartCooldown();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Jump()
    {
        base.Jump();
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Shoot()
    {
        if (facingForward)
        {
            asrce.PlayOneShot(shootSound, 0.7F);
            instance = Instantiate(projectile, new Vector3(transform.position.x +2f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(new Vector3(1.0f, -0.5f, 0.0f) * shootForce);
            instance2 = Instantiate(projectile, new Vector3(transform.position.x + 2f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            instance2.GetComponent<Rigidbody2D>().AddForce(new Vector3(1.0f, 0.5f, 0.0f) * shootForce);
            instance3 = Instantiate(projectile, new Vector3(transform.position.x + 2f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            instance3.GetComponent<Rigidbody2D>().AddForce(new Vector3(1.0f, 0.0f, 0.0f) * shootForce);
            StartCoroutine(DestroyProjectile());
            StartCoroutine(Cooldown());
        }
        else
        {
            asrce.PlayOneShot(shootSound, 0.7F);
            instance = Instantiate(projectile, new Vector3(transform.position.x - 2f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1.0f, -0.5f, 0.0f) * shootForce);
            instance2 = Instantiate(projectile, new Vector3(transform.position.x - 2f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            instance2.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1.0f, 0.5f, 0.0f) * shootForce);
            instance3 = Instantiate(projectile, new Vector3(transform.position.x - 2f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            instance3.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * shootForce);
            StartCoroutine(DestroyProjectile());
            StartCoroutine(Cooldown());
        }

    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        Destroy(instance.gameObject);
        Destroy(instance2.gameObject);
        Destroy(instance3.gameObject);
    }

    public override void Recharge()
    {
        base.Recharge();
    }

    public override void AssignText()
    {
        base.AssignText();
    }

    public override void SpecialMove()
    {
        if ((Input.GetKeyDown(KeyCode.K) && isPlayer1) || (Input.GetKeyDown(KeyCode.S) && !isPlayer1))
        {
            if (canShootSpecial && facingForward)
            {
                asrce.PlayOneShot(specialSound, 0.7F);
                GameObject proj = Instantiate(specialProjectile, new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z), Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootForce/3);
                proj.GetComponent<CliffordProjectile>().player = this.gameObject;
                StartCoroutine(SpecialCooldown());
            }
        }
        if ((Input.GetKeyDown(KeyCode.K) && isPlayer1) || (Input.GetKeyDown(KeyCode.S) && !isPlayer1))
        {
            if (canShootSpecial && !facingForward)
            {
                asrce.PlayOneShot(specialSound, 0.7F);
                GameObject proj = Instantiate(specialProjectile, new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z), Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * shootForce/3);
                proj.GetComponent<CliffordProjectile>().player = this.gameObject;
                StartCoroutine(SpecialCooldown());
            }
        }
    }

    IEnumerator SpecialCooldown()
    {
        canShootSpecial = false;
        yield return new WaitForSeconds(8);
        canShootSpecial = true;
    }

    public override void Shield()
    {
        base.Shield();
    }

    public override IEnumerator Cooldown()
    {
        return base.Cooldown();
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            numberOfJumps = 1;
        }
    }

}
