using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interoperable
{
    [Header("宝箱是否已打开")]
    [SerializeField]
    private bool isOpen;

    [Header("子Image")]
    public SpriteRenderer box1;
    public SpriteRenderer box2;
    public SpriteRenderer box3;
    public SpriteRenderer box4;

    [Header("打开Image")]
    public Sprite spriteOpen1;
    public Sprite spriteOpen2;
    public Sprite spriteOpen3;
    public Sprite spriteOpen4;

    public override void OnTriggerInteroperable(GameObject gameObject)
    {

        Debug.Log("OnTriggerInteroperable");
        // 添加血量
        if (!isOpen)
        {
            isOpen = true;
            box1.sprite = spriteOpen1;
            box2.sprite = spriteOpen2;
            box3.sprite = spriteOpen3;
            box4.sprite = spriteOpen4;
            Character character = gameObject.GetComponent<Character>();
            character.UpdateAddHealth(100);
            //TODO: wmy 补血声音
        }

    }
}
