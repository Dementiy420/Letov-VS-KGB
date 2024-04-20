using UnityEngine;
using UnityEngine.UI;

public class ButtonForm : MonoBehaviour
{
    [Range(0f, 1f)]
    public float AlphaLevel = 1f;
    private Image but;
    void Start()
    {
        but = gameObject.GetComponent<Image>();
        but.alphaHitTestMinimumThreshold = AlphaLevel;
    }

}
