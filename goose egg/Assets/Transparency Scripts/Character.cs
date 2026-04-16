using UnityEngine;

public class Character : MonoBehaviour
{

    private SpriteRenderer m_SpriteRenderer;

    public SpriteRenderer SpriteRenderer => m_SpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }


}
