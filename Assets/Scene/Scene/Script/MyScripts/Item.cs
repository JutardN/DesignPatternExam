using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IObject
{
    [SerializeField] IObject.TYPE type;
    [SerializeField] int amount;
    [SerializeField] int cmptToggle;
    int cmpt;
    public IObject.TYPE Type { get; private set; }

    public int Amount { get; private set; }

    private void Start()
    {
        Type = type;
        Amount = amount;
    }

    public void Interact()
    {
        // si l'item n'est pas une potion, alors c'est une cl� ou une porte et on va les d�sactiver seulement pour pouvoir les r�utiliser si besoin
        gameObject.SetActive(false);
        if(type == IObject.TYPE.POTION)
        {
            // on d�truit la potion car on l'a utilis�e
            Destroy(gameObject);
        }
    }

    public void ToggleOn()
    {
        // si l'on active un toggle on pr�vient la porte et on inncr�mente le compteur pour savoir si le nombre est bon
        cmpt++;
        if(cmpt>= cmptToggle)
        {
            gameObject.SetActive(false);
        }
        Debug.Log(cmpt);

    }

    public void ToggleOff()
    {
        // on retire 1 au compteur lors de l'�teignage d'un toggle
        cmpt--;
        if (cmpt < cmptToggle)
        {
            gameObject.SetActive(true);
        }
        Debug.Log(cmpt);
    }
}
