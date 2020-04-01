using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] idleFrameArray;
    [SerializeField] private Sprite[] runFrameArray;
    [SerializeField] private Sprite[] jumpFrameArray;
    [SerializeField] private Sprite[] deadFrameArray;
    [SerializeField] private Sprite[] attackFrameArray;
    private Sprite[] currentFrameArray;
    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;
    private float frameRate = .05f;
    private string animState;

    private void Update() {
        timer += Time.deltaTime;
        
        if (timer >= frameRate){
            timer = 0f;

            if (animState == "dead" || animState == "jump") {
                if (currentFrame+1 < currentFrameArray.Length){
                    currentFrame = (currentFrame + 1) % currentFrameArray.Length;
                    spriteRenderer.sprite = currentFrameArray[currentFrame];
                }
            } else {
                currentFrame = (currentFrame + 1) % currentFrameArray.Length;
                spriteRenderer.sprite = currentFrameArray[currentFrame];
            }
            
        }
    }

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentFrameArray = idleFrameArray;
        animState = "idle";
    }

    public void AnimateIdle(){
        if (animState != "idle"){
            animState = "idle";
            currentFrame = 0;
            currentFrameArray = idleFrameArray;
        }
    }

    public void AnimateRun(){
        if (animState != "run"){
            animState = "run";
            currentFrame = 0;
            currentFrameArray = runFrameArray;
        }
    }

    public void AnimateJump(){
        if (animState != "jump"){
            animState = "jump";
            currentFrame = 0;
            currentFrameArray = jumpFrameArray;
        }
    }

    public void AnimateDead(){
        if (animState != "dead"){
            animState = "dead";
            currentFrame = 0;
            currentFrameArray = deadFrameArray;
        }
    }

    public void AnimateAttack(){
        if (animState != "attack"){
            animState = "attack";
            currentFrame = 0;
            currentFrameArray = attackFrameArray;
        }
    }
}
