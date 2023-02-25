using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
{
    [SerializeField] private CharacterDatabase _characterDatabase;
    [SerializeField] private Transform _characterSpawnPoint;
    //[SerializeField] private CollectorController[] _collectorControllers;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        foreach (var client in ServerManager.Instance.ClientData)
        {
            var character = _characterDatabase.GetCharacterById(client.Value.CharacterId);
            if (character != null)
            {
                //var spawnPos = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
                var characterInstance = Instantiate(character.GamePlayPrefab, _characterSpawnPoint);
                characterInstance.SpawnAsPlayerObject(client.Value.ClientId);
               
            }
        }
    }
}
