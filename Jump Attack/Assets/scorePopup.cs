using TMPro;
using UnityEngine;

public class scorePopup : MonoBehaviour
{
    public Color textColor;
    public int score;
    public TextMeshPro TMZ;
    // Start is called before the first frame update
    private void Start()
    {
        TMZ.color = textColor;
        TMZ.text = "+" + score.ToString();

        LeanTween.moveLocalY(this.gameObject, transform.position.y + .2f, 0.3f);

        Destroy(gameObject, 0.5f);
    }
}
