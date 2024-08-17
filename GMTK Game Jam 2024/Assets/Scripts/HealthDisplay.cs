using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [Foldout("Layers")] public Image healthBar;
    [Foldout("Layers")] public Image changeHealthBar;
    [Foldout("Layers")] public Image healHealthBar;

    public HealthSystem healthSystem;
    [Space(10)]

    [ColorUsage(true, true), BoxGroup("Layer Visuals")] public Color tookDamageColor = Color.red;
    [BoxGroup("Layer Visuals")] public LeanTweenType tookDamageTween;

    [ColorUsage(true, true), BoxGroup("Layer Visuals")] public Color healedColor = Color.green;
    [BoxGroup("Layer Visuals")] public LeanTweenType healedTween;

    public float chipAwaySpeed = 1f;




    private void Awake()
    {
        healthBar.fillMethod = Image.FillMethod.Horizontal;
        changeHealthBar.fillMethod = Image.FillMethod.Horizontal;
        healHealthBar.fillMethod = Image.FillMethod.Horizontal;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBarUI();
    }
    void UpdateBarUI()
    {
        float fill_Health = healthBar.fillAmount; // bar for health
        float fill_Change = changeHealthBar.fillAmount; // bar for chip away effect
        float fill_Heal = healHealthBar.fillAmount; // bar for accumulated healing

        float healthFraction = healthSystem.currentHealth / healthSystem.maxHealth;
        


        if (fill_Change > healthFraction)
        {
            healthBar.fillAmount = healthFraction;
            changeHealthBar.color = tookDamageColor;

            LeanTween.value(changeHealthBar.gameObject, fill_Change, healthFraction, chipAwaySpeed)
                .setEase(tookDamageTween)
                .setOnUpdate((value) =>
                {
                    changeHealthBar.fillAmount = value;
                });
        }

        if (fill_Health < healthFraction)
        {
            changeHealthBar.fillAmount = healthFraction;
            changeHealthBar.color = healedColor;

            LeanTween.value(changeHealthBar.gameObject, fill_Health, changeHealthBar.fillAmount, chipAwaySpeed)
                .setEase(healedTween)
                .setOnUpdate((value) =>
                {
                    changeHealthBar.fillAmount = value;
                });
        }
    }

}
