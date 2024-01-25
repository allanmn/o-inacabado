using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int id, maxHealth, currentHealth, damage, currentDamage, moveSpeed, currentMoveSpeed;
    protected string enemyName;
    private bool isPlayingHitSound = false;
    private float timeBetweenHits = .2f;
    public bool isDestroyed = false;
    public bool isImmune = false;
    AudioListController audioListController;
    private IEnumerator enemyHitSoundTimer;
    public UIController uiController;

    void Start()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
        audioListController = GameObject.Find("AudioController").GetComponent<AudioListController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit(Collider2D other, int damage)
    {

        if (other.CompareTag("Player") && !isImmune)
        {

            if (!isPlayingHitSound)
            {
                isPlayingHitSound = true;
                audioListController.effectsSource.PlayOneShot(audioListController.effects[Random.Range(6, 7)].audioclip);
                StartCoroutine(EnemyHitSoundTimer(timeBetweenHits));
            }
            currentHealth -= damage;
            // healthBar.GetComponent<StatusBar>().UpdateStatusBar(currentHealth);
            if (currentHealth <= 0 && !isDestroyed)
            {
                audioListController.effectsSource.PlayOneShot(audioListController.effects[5].audioclip, .5f);
                Destroy();
            }

        }
    }

    public void DamagePlayer(int damage)
    {
        uiController.DecreaseShieldHits(damage);
    }

    public void HitDestroyer(Collider2D other, int damage)
    {
        if (other.gameObject.CompareTag("Destroyer"))
        {
            DamagePlayer(damage);
            Destroy();
        }
    }

    IEnumerator EnemyHitSoundTimer(float timeBetweenHits)
    {
        yield return new WaitForSeconds(timeBetweenHits);
        isPlayingHitSound = false;
    }

    public void Destroy(float timeUntilDestroyed = 0)
    {
        isDestroyed = true;
        Destroy(gameObject, timeUntilDestroyed);
    }

    public void Movement()
    {
        transform.position += new Vector3(0, -1 * moveSpeed * Time.deltaTime, 0);
    }
}
