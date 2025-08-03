using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{    
    [SerializeField] private GameObject _hpPrefab;

    public void HPUpdate(int hp)
    {
        int childNum = transform.childCount;
        if (hp < childNum)
        {
            // Decrease HP
            if (hp < 0)
            {
                return;
            }
            for (int i = 0; i < childNum - hp; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        else if (hp > childNum)
        {
            // Increase HP
            for (int i = 0; i < hp - childNum; i++)
            {
                Instantiate(_hpPrefab, this.transform);
            }
        }
        else
        {
            return;
        }
    }
}
