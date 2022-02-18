using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObject
{
    enum TYPE {KEY,POTION,DOOR};

    void Interact();
}
