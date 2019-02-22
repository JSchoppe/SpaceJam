﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum RoomName
    {
        ENGINE,
        CREWQUARTERS,
        LIFESUPPORT,
        GENERATOR
    }

    // This is where the designer specifies
    // the location of the collectible.
    [SerializeField]
    private RoomName room;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Retrieve the player progress script from the colliding player.
        PlayerProgress prog = collision.gameObject.GetComponent<PlayerProgress>();

        // Update the players progress for a room.
        switch (room)
        {
            case RoomName.CREWQUARTERS:
                prog.CrewQuartersCollectibles++;
                break;
            case RoomName.ENGINE:
                prog.EngineRoomCollectibles++;
                break;
            case RoomName.GENERATOR:
                prog.GeneratorRoomCollectibles++;
                break;
            case RoomName.LIFESUPPORT:
                prog.LifeSupportCollectibles++;
                break;
        }

        // Remove this collectible.
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
