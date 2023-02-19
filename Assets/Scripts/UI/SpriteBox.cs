using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class SpriteBox : MonoBehaviour
{
    public Image boxImage;
    public Image spriteImage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void FlashBackgroundColor(Color color, int ms)
    {
        Color prior = boxImage.color;
        boxImage.color = color;
        await Task.Delay(ms);
        boxImage.color = prior;
    }

    public void SetBackgroundColor(Color color)
    {
        boxImage.color = color;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteImage.sprite = sprite;
    }

    public async void FlashSprite(Sprite sprite, int ms)
    {
        Sprite prior = spriteImage.sprite;
        spriteImage.sprite = sprite;
        await Task.Delay(ms);
        spriteImage.sprite = prior;
    }
}
