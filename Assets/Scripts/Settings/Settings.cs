using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnim {
   public static string kEnemyAnimIsWalk = "isWalk";
   public static string kEnemyAnimIsRun = "isRun";
   public static string kEnemyAnimDamageTrig = "damageTrig";
}
public class CharacterAnim 
{
   // public static string kPlayerAnimIsWalking = "isWalking";
   // public static string kPlayerAnimIsRuning = "isRuning";
   /// <summary>
   /// Player动画参数 x方向速度
   /// </summary>
   public static string kCharacterAnimVelocityX = "velocityX";
   public static string kCharacterAnimVelocityY = "velocityY";
   
   //TODO: wmy 走路和跑步的速度
   /// <summary>
   /// Player动画参数 发动受伤害
   /// </summary>
   public static string kCharacterAnimDamageTrig = "damageTrig";
   /// <summary>
   /// Player动画参数 玩家是否死亡
   /// </summary>
   public static string kCharacterAnimIsDead = "isDead";
   public static string kCharacterAnimDeadTrig = "deadTrig";
   
   
   
   public static string kCharacterAnimIsOnGround = "isOnGround";
}

public class PlayerAnim {
   public static string kPlayerAnimIsOnGround = "isOnGround";
   /// <summary>
   /// Player动画参数 触发跳跃
   /// </summary>
   public static string kPlayerAnimJumpTrig = "jumpTrig";
   public static string kPlayerAnimIsJump = "isJump";

   public static string kPlayerAnimIsAttack = "isAttack";
   public static string kPlayerAnimAttackCount = "attackCount";
   public static string kPlayerAnimAttackTrig = "attackTrig";
}


public class MYTag {
   public static string kTagGround = "Ground";
}
