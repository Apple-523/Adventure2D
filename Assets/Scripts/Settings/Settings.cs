using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings 
{
   // public static string kPlayerAnimIsWalking = "isWalking";
   // public static string kPlayerAnimIsRuning = "isRuning";
   /// <summary>
   /// Player动画参数 x方向速度
   /// </summary>
   public static string kPlayerAnimVelocityX = "velocityX";
   public static string kPlayerAnimVelocityY = "velocityY";
   
   //TODO: wmy 走路和跑步的速度
   /// <summary>
   /// Player动画参数 发动受伤害
   /// </summary>
   public static string kPlayerAnimDamageTrig = "damageTrig";
   /// <summary>
   /// Player动画参数 玩家是否死亡
   /// </summary>
   public static string kPlayerAnimIsDead = "isDead";
   /// <summary>
   /// Player动画参数 触发跳跃
   /// </summary>
   public static string kPlayerAnimJumpTrig = "jumpTrig";
   public static string kPlayerAnimIsJump = "isJump";
   
   
   public static string kPlayerAnimIsOnGround = "isOnGround";
}

public class MYTag {
   public static string kTagGround = "Ground";
}
