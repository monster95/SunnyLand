using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Death()
    {
        Destroy(this.gameObject, 0.2f);
    }
}
