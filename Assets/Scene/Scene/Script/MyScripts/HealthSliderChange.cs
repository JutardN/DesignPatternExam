using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthSliderChange : MonoBehaviour
{
    [SerializeField] PlayerEntity player;

    private void Start()
    {
        player.Health.OnDamage += ChangeSlider;
        player.Health.OnHeal += ChangeSlider;
    }

    private void OnDestroy()
    {
        player.Health.OnDamage -= ChangeSlider;
        player.Health.OnHeal -= ChangeSlider;
    }

    private void ChangeSlider(int obj)
    {
        TryGetComponent<Slider>(out Slider slider);
        if (slider)
        {
            slider.value = (float)player.Health.CurrentHealth / player.Health.MaxHealth;
        }
    }
}
