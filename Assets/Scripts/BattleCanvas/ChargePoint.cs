using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChargePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Point").GetComponent<Image>().color =
            transform.parent.parent.GetComponent<Ability>().ColorAb;
        gameObject.transform.Find("Point").gameObject.SetActive(false);
    }
}
