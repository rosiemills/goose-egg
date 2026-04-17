using UnityEngine;

public class RendererFader : MonoBehaviour
{
    private SpriteRenderer[] m_SpriteRenderers;
    private bool m_FadeOutEnabled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if(m_FadeOutEnabled)
        {
            FadeOut();
        }
        else
        {
            FadeIn();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_FadeOutEnabled) return;

        if(collision.TryGetComponent<Character>(out var character))
        {
            m_FadeOutEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!m_FadeOutEnabled) return;

        if(collision.TryGetComponent<Character>(out var character))
        {
            m_FadeOutEnabled = false;
        }
    }

    private void FadeOut()
    {
        foreach (var renderer in m_SpriteRenderers)
        {
            ChangeOpacity(renderer, 0.3f);
        }
    }
    
    private void FadeIn()
    {
        foreach(var renderer in m_SpriteRenderers)
        {
            ChangeOpacity(renderer, 1);
        }
    }

    private void ChangeOpacity(SpriteRenderer renderer, float targetAlpha)
    {
        Color color = renderer.color;
        Color smoothColor = new(color.r, color.g, color.b, Mathf.MoveTowards(color.a, targetAlpha, Time.deltaTime * 2) );
        renderer.color = smoothColor;
    }

}
