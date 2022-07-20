using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; //trap trigger
    private bool active; //when trap is actived and can hurt the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActiveFiretrap());

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private IEnumerator ActiveFiretrap()
    {
        //turn the sprite red and trigger the trap
        triggered = true;
        spriteRend.color = Color.red;

        //wait for delay, active the trap, turn on animation, return color to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //reset all variables and animator and deactivated the trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);

    }
}
