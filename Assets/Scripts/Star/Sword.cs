using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Sword : MonoBehaviour
{
    private Animator swordAnim;
    private AudioSource swordAudio;
    public AudioClip slashClip;
    public GameObject gun;
    public BattleManager battleManager;
    public int speedStat = 5;
    public int health = 10;
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        swordAnim = GetComponent<Animator>();
        swordAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gun.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.GetComponent<Enemy>();
        }
    }

    public void Attack()
    {
        if (battleManager.state == BattleManager.BattleState.PLAYERTURN && Input.GetKeyDown(KeyCode.Space))
        {
            swordAnim.SetTrigger("Attack_trig");
            swordAudio.PlayOneShot(slashClip);
            Debug.Log("You hit something!");
            enemy.health -= 1;
            battleManager.state = BattleManager.BattleState.ENEMYTURN;
            Debug.Log("Now Enemy Turn");
        }

    }
}
