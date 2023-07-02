using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_PropEntity : MonoBehaviour {

    int id;
    public int ID => id;
    int typeID;
    public int TypeID => typeID;

    public void TearDown() {
        Destroy(gameObject);
    }

}