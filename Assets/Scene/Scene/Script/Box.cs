using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, ITouchable
{
    [SerializeField] Item potion;

    public void Touch(int power)
    {
        // Random de spawn de potion
        int tmp = Random.Range(0, 3);
        if (tmp == 0)
        {
            Item _potion = Instantiate(potion, transform.position,Quaternion.identity);
            _potion.transform.position = new Vector3(_potion.transform.position.x, _potion.transform.position.y, -3);
        }
        Destroy(gameObject);
    }
}
