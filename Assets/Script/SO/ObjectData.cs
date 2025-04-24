using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ObjectData_", menuName = "Data/ObjectData")]
public class ObjectData : ScriptableObject
{

    public E_PoolName poolName;
    public GameObject pref;
    public int numberAvail;
}