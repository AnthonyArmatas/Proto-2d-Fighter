using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    //Passed in an amount and bar to manipulate
    public Image currentHealthBar;
    public Text ratioText;
    public bool isDead = false;

    private float hitPoints = 150;
    private float maxHitPoints = 150;
    private Animator tor;

	// Use this for initialization
    private void Start()
    {
        tor = GetComponent<Animator>();

	}
	
	// Update is called once per frame
    private void UpdateHealthBar()
    {
        float ratio = hitPoints / maxHitPoints;
        //Makes the health bar decrease in relation to the health lost
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio*100).ToString("0") + '%';
    }

    private void TakeDamage(float damage)
    {
        // MouseLook Charcontdisable = GetComponent<MouseLook>().enabled = false;

        //Calls the PlayerController script to stagger the player when hit.
        //PlayerController playerScrpt = GetComponent<PlayerController>();
        genericPlayerController gplayerScrpt = GetComponent<genericPlayerController>();
        gplayerScrpt.PlayerStagger();

        hitPoints -= damage;
        if (hitPoints == 0)
        {
            tor.SetBool("Dead", true);
            isDead = true;
            Debug.Log("Dead!");
        }
        if (hitPoints < 0)
        {
            hitPoints = 0;
        }
        UpdateHealthBar();
    }

    private void TakeIceDamage()
    {
        //Since I will be getting rid of the health component of the game, the HealthScript will be that in name only.
        //It will fuction as a status controller. 
        //Calls the genericPlayerController script to slow the player when hit.
        genericPlayerController gplayerScrpt = GetComponent<genericPlayerController>();

        gplayerScrpt.PlayerSlowed();

    }

    private void HealDamage(float heal)
    {
        hitPoints += heal;
        if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
        UpdateHealthBar();
    }
}
