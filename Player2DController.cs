using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player2DController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public GameObject shield;
    public GameObject projectile;
    public GameObject guard;
    public GameObject fart;
    public GameObject fartInstance;
    public GameObject letterF;

    public float moveSpeed = 60.0f;

    public float jumpHeight = 470f;
    public int numberOfJumps = 2;

    public bool facingForward = true;

    public int ammo = 3;
    public int maxAmmo = 3;
    float reloader = 0f;
    public float reloadSize = 3f;
    public float shootForce = 300.0f;
    public bool canShoot = true;

    public int hp = 6;
    public int cd = 1;

    public bool holdingShield = false;

    public bool isPlayer1;

    public float shieldDistance = 1f;

    public bool canUseFart = true;

    public Text hpText;
    public Text hpText2;
    public Text ammoText;
    public Text ammoText2;

    public AudioSource asrce;
    public AudioClip shieldAudio;
    public AudioClip jumpAudio;
    public AudioClip shootAudio;

    public virtual void Start()
    {
        asrce = GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        AssignText();
        UpdateHPText();
        UpdateAmmoText();
        Die();
        Move();
        Jump();
        if ((Input.GetKeyDown(KeyCode.U) && isPlayer1) || (Input.GetKeyDown(KeyCode.Q) && !isPlayer1))
        {
            if (ammo != 0 && canShoot)
            {
                ammo--;
                {
                    Shoot();
                }
            }
        }
        Shield();
        Fart();
        SpecialMove();
        Recharge();
    }

    public virtual void AssignText()
    {
        if (isPlayer1)
        {
            hpText = GameObject.Find("hptext").GetComponent<Text>();
            ammoText = GameObject.Find("ammotext").GetComponent<Text>();
        }
        if (!isPlayer1)
        {
            hpText2 = GameObject.Find("hptext2").GetComponent<Text>();
            ammoText2 = GameObject.Find("ammotext2").GetComponent<Text>();
        }
    }

    public virtual void UpdateHPText()
    {
        if (isPlayer1)
        {
            hpText.text = "HP: " + hp.ToString();
        }
        else
        {
            hpText2.text = "HP: " + hp.ToString();
        }
    }

    public virtual void UpdateAmmoText()
    {
        if (isPlayer1)
        {
            ammoText.text = "Ammo: " + ammo.ToString();
        }
        else
        {
            ammoText2.text = "Ammo: " + ammo.ToString();
        }
    }

    public virtual void Fart()
    {
        if ((isPlayer1 && Input.GetKeyDown(KeyCode.J)) && canUseFart|| (!isPlayer1 && Input.GetKeyDown(KeyCode.A)) && canUseFart)
        {
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

    public virtual IEnumerator FartCooldown()
    {
        canUseFart = false;
        yield return new WaitForSecondsRealtime(0.6f);
        Destroy(fartInstance.gameObject);
        yield return new WaitForSeconds(5);
        canUseFart = true;
    }

    public virtual void Die()
    {
        if (hp <= 0)
        {
            Instantiate(letterF, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public virtual void Shield()
    {
        if ((isPlayer1 && Input.GetKeyDown(KeyCode.O))|| (!isPlayer1 && Input.GetKeyDown(KeyCode.E)))
        {
            if (facingForward)
            {
                asrce.PlayOneShot(shieldAudio, 0.7F);
                guard = Instantiate(shield, new Vector3(transform.position.x + shieldDistance, transform.position.y, transform.position.z), Quaternion.identity);
                guard.transform.parent = gameObject.transform;
                holdingShield = true;
                canShoot = false;
            }
            else if (!facingForward)
            {
                asrce.PlayOneShot(shieldAudio, 0.7F);
                guard = Instantiate(shield, new Vector3(transform.position.x - shieldDistance, transform.position.y, transform.position.z), Quaternion.identity);
                guard.transform.parent = gameObject.transform;
                holdingShield = true;
                canShoot = false;
            }
        }
        if ((isPlayer1 && Input.GetKeyUp(KeyCode.O))|| (!isPlayer1 && Input.GetKeyUp(KeyCode.E)))
        {
            Destroy(guard.gameObject);
            holdingShield = false;
            canShoot = true;
        }
    }
  
    public virtual void SpecialMove()
    {
    }

    public virtual void Shoot()
    {    
            if (facingForward)
            {
                asrce.PlayOneShot(shootAudio, 0.7F);
                GameObject proj = Instantiate(projectile, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootForce);
                StartCoroutine(Cooldown());
            }
            else
            {
            asrce.PlayOneShot(shootAudio, 0.7F);
            GameObject proj = Instantiate(projectile, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * shootForce);
                StartCoroutine(Cooldown());
            }
    }

    public virtual IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cd);
        canShoot = true;
    }

    public virtual void Recharge()
    {
        if(reloader >= reloadSize)
        {
            reloader = 0f;
            ammo = maxAmmo;
        }
        if ((Input.GetKey(KeyCode.PageDown) && isPlayer1)|| (Input.GetKey(KeyCode.D) && !isPlayer1))
        {
            if (ammo == 0)
            {
                reloader += Time.deltaTime;
            }
        }
    }

    public virtual void Move()
    {
        if ((Input.GetKey(KeyCode.RightArrow) && isPlayer1)|| (Input.GetKey(KeyCode.B) && !isPlayer1))
        {
            facingForward = true;
            transform.Translate(moveSpeed/1000, 0, 0);
            sr.flipX = false;
            if (holdingShield && guard)
            {
                guard.transform.position = new Vector3(transform.position.x + shieldDistance, transform.position.y, transform.position.z);
            }
        }
        if ((Input.GetKey(KeyCode.LeftArrow) && isPlayer1)|| (Input.GetKey(KeyCode.C) && !isPlayer1))
        {
            facingForward = false;
            transform.Translate(-moveSpeed / 1000, 0, 0);
            sr.flipX = true;
            if (holdingShield && guard)
            {
                guard.transform.position = new Vector3(transform.position.x - shieldDistance, transform.position.y, transform.position.z);
            }
        }
    }

    public virtual void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.I) && isPlayer1)|| (Input.GetKeyDown(KeyCode.W) && !isPlayer1))
        {
            if (numberOfJumps > 0)
            {
                asrce.PlayOneShot(jumpAudio, 0.7F);
                rb.AddForce(Vector2.up * jumpHeight);
                numberOfJumps--;
            }
        }
    }

    public virtual void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            numberOfJumps = 2;
        }
    }
}
