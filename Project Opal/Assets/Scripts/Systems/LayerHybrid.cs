using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHybrid : MonoBehaviour {

    public float layer_order_multiplier = 10;
    SpriteRenderer m_spriteRenderer;

    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update () {
        AdjustLayering();
	}

    void AdjustLayering()
    {
        m_spriteRenderer.sortingOrder = (int)(transform.position.y * -layer_order_multiplier);
    }

}
