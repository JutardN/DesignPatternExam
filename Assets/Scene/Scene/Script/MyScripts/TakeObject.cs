using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour
{
    [SerializeField] PhysicEvent2D _radius;
    [SerializeField] PlayerEntity _player;

    public List<Item> _objects;


    private void Start()
    {
        _objects = new List<Item>();
        _radius.TriggerEnter2D += _radius_TriggerEnter2D;
    }

    private void OnDestroy()
    {
        _radius.TriggerEnter2D -= _radius_TriggerEnter2D;

    }

    private void _radius_TriggerEnter2D(Collider2D arg0)
    {
        if (arg0.transform.parent)
        {
            if (arg0.transform.parent.TryGetComponent(out Item e))
            {
                // si l'item est une potion alors on la rajoute à la liste et on la désactive
                if(e.Type == IObject.TYPE.KEY)
                {
                    _objects.Add(e);
                    e.Interact();
                }
                // si c'est une potion on l'utilise
                if (e.Type == IObject.TYPE.POTION)
                {
                    e.Interact();
                    _player.Health.RegenHP(e.Amount);
                }
                // S'il y a au moins 1 élément, alors ce sera une clé car on ne récupére que les clés
                if (e.Type == IObject.TYPE.DOOR && _objects.Count!=0)
                {
                    e.Interact();
                    _objects.RemoveAt(0);
                }
            }
            else
            {
                // Si le trigger est mis au même endroit que le script
                if(arg0.TryGetComponent(out Item _e))
                {
                    if (e.Type == IObject.TYPE.KEY)
                    {
                        _objects.Add(e);
                        e.Interact();
                    }
                    if (e.Type == IObject.TYPE.POTION)
                    {
                        e.Interact();
                        _player.Health.RegenHP(e.Amount);
                    }
                    if (e.Type == IObject.TYPE.DOOR && _objects.Count != 0)
                    {
                        e.Interact();
                        _objects.RemoveAt(0);
                    }
                }
            }
        }
    }
}
