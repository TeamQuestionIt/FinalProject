using UnityEngine;
using System.Collections;

public class ImpAttack : MonoBehaviour {

    public BoxCollider2D hitBox;
    public int Damage { get; private set; }

    public bool IsHitBox(Collider2D col)
    {
        BoxCollider2D b = col as BoxCollider2D;
        if (null == b)
        {
            return false;
        }
        return b.size == hitBox.size && b.offset == hitBox.offset;
    }

	// Use this for initialization
	void Start () {
        Damage = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D col)
    {

    }
}
