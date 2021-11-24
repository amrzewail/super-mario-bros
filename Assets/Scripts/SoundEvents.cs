using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundEvents
{

    public static Action<Sounds> Play;

    public static Action StopBackground;


}

public enum Sounds
{
    GameOver,
    Die,
    OutOfTime,
    StageClear,
    LiveUp,
    BrickSmash,
    Bump,
    Coin,
    Flagpole,
    Fireball,
    JumpSmall,
    JumpSuper,
    Kick,
    Pipe,
    PowerUp,
    PowerUpAppears,
    Stomp
}
