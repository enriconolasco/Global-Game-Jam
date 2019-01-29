using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreekGuy : Player2DController
{
    public GameObject specialProjectile;
    bool canShootSpecial = true;
    bool canFart = true;

    public AudioClip specialAudio;
    public AudioClip potAudio;

    public void Reset()
    {

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
        SpecialMove2();
        base.Update();
    }

    public override void Fart()
    {
        if ((isPlayer1 && Input.GetKeyDown(KeyCode.J)) && canFart || (!isPlayer1 && Input.GetKeyDown(KeyCode.A)) && canFart)
        {
            asrce.PlayOneShot(potAudio, 0.7F);
            canFart = false;
            fartInstance = Instantiate(fart, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), Quaternion.identity);
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
        yield return new WaitForSeconds(4);
        Destroy(fartInstance.gameObject);
        yield return new WaitForSeconds(2);
        canFart = true;
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Shield()
    {
        base.Shield();
    }

    void SpecialMove2()
    {

    }

    public override void AssignText()
    {
        base.AssignText();
    }

    public override void SpecialMove()
    {
        if ((Input.GetKeyDown(KeyCode.K) && isPlayer1)|| (Input.GetKeyDown(KeyCode.S) && !isPlayer1))
        {
            if (canShootSpecial && facingForward)
            {
                asrce.PlayOneShot(specialAudio, 0.7F);
                GameObject proj = Instantiate(specialProjectile, new Vector3(transform.position.x + 1f, transform.position.y - 0.4f, transform.position.z), Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootForce * 1.4f);
                StartCoroutine(SpecialCooldown());
            }
        }
        if ((Input.GetKeyDown(KeyCode.K) && isPlayer1)|| (Input.GetKeyDown(KeyCode.S) && !isPlayer1))
        {
            if (canShootSpecial && !facingForward)
            {
                asrce.PlayOneShot(specialAudio, 0.7F);
                GameObject proj = Instantiate(specialProjectile, new Vector3(transform.position.x - 1f, transform.position.y - 0.4f, transform.position.z), Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * shootForce * 1.4f);
                StartCoroutine(SpecialCooldown());
            }
        }
    }

    IEnumerator SpecialCooldown()
    {
        canShootSpecial = false;
        yield return new WaitForSeconds(2);
        canShootSpecial = true;
    }
    
    public override void Shoot()
    {
        if (facingForward)
        {
            asrce.PlayOneShot(shootAudio, 0.7F);
            GameObject proj = Instantiate(projectile, new Vector3(transform.position.x + 1f, transform.position.y + 0.3f, transform.position.z), Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().AddForce(new Vector2(2f, 1f) * shootForce);
            StartCoroutine(Cooldown());
        }
        else
        {
            asrce.PlayOneShot(shootAudio, 0.7F);
            GameObject proj = Instantiate(projectile, new Vector3(transform.position.x - 1f, transform.position.y + 0.3f, transform.position.z), Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2f, 1f) * shootForce);
            StartCoroutine(Cooldown());
        }
    }

    public override IEnumerator Cooldown()
    {
        return base.Cooldown();
    }

    public override void Recharge()
    {
        base.Recharge();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Jump()
    {
        base.Jump();
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
    }
}
