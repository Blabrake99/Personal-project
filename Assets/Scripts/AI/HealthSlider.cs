using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private Slider HealthBar;
    private Image targetBar;
    // Start is called before the first frame update
    void Awake()
    {
        HealthBar = gameObject.GetComponent<Slider>();
        //this makes it so we can easily change the color of 
        //the slider bar image 
        if(HealthBar.fillRect != null)
        {
            targetBar = HealthBar.fillRect.GetComponent<Image>();
        }
    }
    void FigureOutHealthBarColor()
    {
        //simple calculation to find hp percent 
        var currentHealthPercent = (health * 100) / maxHealth;
        //this just adds a little spice to the health bar 
        //it'll change color based on the percent left on the Unit
        if(currentHealthPercent >= 75)
        {
            targetBar.color = Color.green;
        } else if(currentHealthPercent < 75 && currentHealthPercent >= 25)
        {
            targetBar.color = Color.yellow;
        } else if(currentHealthPercent < 25)
        {
            targetBar.color = Color.red;
        }
    }

    public void SetHealth(int _maxHealth, int _currentHealth)
    {
        maxHealth = _maxHealth;
        health = _currentHealth;

        HealthBar.maxValue = maxHealth;
        HealthBar.value = health;
        FigureOutHealthBarColor();
    }
}
