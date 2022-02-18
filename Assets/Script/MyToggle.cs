using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyToggle : MonoBehaviour, ITouchable
{
    // Je veux ouvrir un évènement pour les designers pour qu'ils puissent set la couleur du sprite eux même
    [SerializeField] UnityEvent _onToggleOn;
    [SerializeField] UnityEvent _onToggleOff;
    public event UnityAction ToggleActionOn { add => _onToggleOn.AddListener(value); remove => _onToggleOn.RemoveListener(value); }
    public event UnityAction ToggleActionOff { add => _onToggleOff.AddListener(value); remove => _onToggleOff.RemoveListener(value); }

    [SerializeField] Item Door;

    public bool IsActive { get; private set; }

    private void Start()
    {
        ToggleActionOn += Door.ToggleOn;
        ToggleActionOff += Door.ToggleOff;
    }

    private void OnDestroy()
    {
        ToggleActionOn -= Door.ToggleOn;
        ToggleActionOff -= Door.ToggleOff;
    }

    public void Touch(int power)
    {
        IsActive = !IsActive;
        if (IsActive)
        {
            _onToggleOn?.Invoke();
        }
        else
        {
            _onToggleOff?.Invoke();
        }
    }
}
