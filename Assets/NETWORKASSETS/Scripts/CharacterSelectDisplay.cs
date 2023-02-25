using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSelectDisplay : NetworkBehaviour
{
    [SerializeField] private CharacterDatabase _characterDatabase;
    [SerializeField] private Transform _characterHolder;
    [SerializeField] private CharacterSelectButton _selectButtonPrefab;
    [SerializeField] private PlayerCard[] _playerCards;
    [SerializeField] private GameObject _characterInfoPannel;
    [SerializeField] private TMP_Text _characterNameText;
    [SerializeField] private Transform _introSpawnPoint;
    [SerializeField] private Button _lockInButton;

    private GameObject _introInstance;
    private List<CharacterSelectButton> _characterButtons = new List<CharacterSelectButton>();
    private NetworkList<CharacterSelectState> _players;


    private void Awake()
    {
        _players = new NetworkList<CharacterSelectState>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            PlayerSkin[] allCharacters = _characterDatabase.GetAllCharacters();

            foreach (var character in allCharacters)
            {
                var selectButtonInstance = Instantiate(_selectButtonPrefab, _characterHolder);
                selectButtonInstance.SetCharacter(this, character);
                _characterButtons.Add(selectButtonInstance);
            }

            _players.OnListChanged += HandlePlayersStateChanged;
        }

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClienConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += HandleClienDisconnected;


            foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
            {
                HandleClienConnected(client.ClientId);
            }
        }
    }
    public override void OnNetworkDespawn()
    {
        if (IsClient)
        {
            _players.OnListChanged -= HandlePlayersStateChanged;
        }

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClienConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClienDisconnected;

        }
    }

    private void HandleClienConnected(ulong clientId)
    {
        _players.Add(new CharacterSelectState(clientId));
    }

    private void HandleClienDisconnected(ulong clientId)
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i].ClientId == clientId)
            {
                _players.RemoveAt(i);
                break;
            }
        }
    }

    public void Select(PlayerSkin character)
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i].ClientId != NetworkManager.Singleton.LocalClientId) { continue; }

            if (_players[i].IsLockedIn) { return; }

            if (_players[i].CharacterId == character.NetworkId) { return; }

            if (IsCharacterTaken(character.NetworkId, false)) { return; }
        }

        _characterNameText.text = character.DisplayName;

        _characterInfoPannel.SetActive(true);

        if (_introInstance != null)
        {
            Destroy(_introInstance);
        }

        _introInstance = Instantiate(character.GetPrefab(), _introSpawnPoint);

        SelectServerRpc(character.NetworkId);
    }

    public void LockIn()
    {
        LockInServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void LockInServerRpc(ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i].ClientId != serverRpcParams.Receive.SenderClientId) { continue; }

            if (!_characterDatabase.IsValidCharacterId(_players[i].CharacterId)) { return; }

            if (IsCharacterTaken(_players[i].CharacterId, true)) { return; }

            _players[i] = new CharacterSelectState(
                _players[i].ClientId,
                _players[i].CharacterId,
                true
                );

        }

        foreach (var player in _players)
        {
            if (!player.IsLockedIn) { return; }
        }

        foreach (var player in _players)
        {
            ServerManager.Instance.SetCharacter(player.ClientId, player.CharacterId);
        }

        ServerManager.Instance.StartGame();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SelectServerRpc(int characterId, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i].ClientId != serverRpcParams.Receive.SenderClientId) { continue; }

            if (!_characterDatabase.IsValidCharacterId(characterId)) { return; }

            if (IsCharacterTaken(characterId, true)) { return; }

            _players[i] = new CharacterSelectState
                (
                _players[i].ClientId,
                characterId,
                _players[i].IsLockedIn
                );
        }
    }

    private void HandlePlayersStateChanged(NetworkListEvent<CharacterSelectState> changeEvent)
    {
        for (int i = 0; i < _playerCards.Length; i++)
        {
            if (_players.Count > i)
            {
                _playerCards[i].UpdateDisplay(_players[i]);
            }
            else
            {
                _playerCards[i].DisableDisplay();
            }
        }

        foreach (var button in _characterButtons)
        {
            if (button.IsDisabled) { continue; }

            if (IsCharacterTaken(button.Character.NetworkId, false))
            {
                button.SetDisabled();
            }
        }

        foreach (var player in _players)
        {
            if (player.ClientId != NetworkManager.Singleton.LocalClientId) { continue; }

            if (player.IsLockedIn)
            {
                _lockInButton.interactable = false;
                break;
            }

            if (IsCharacterTaken(player.CharacterId, false))
            {
                _lockInButton.interactable = false;
                break;
            }

            _lockInButton.interactable = true;
            break;
        }
    }

    private bool IsCharacterTaken(int characterId, bool checkAll)
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (!checkAll)
            {
                if (_players[i].ClientId == NetworkManager.Singleton.LocalClientId) { continue; }
            }

            if (_players[i].IsLockedIn && _players[i].CharacterId == characterId)
            {
                return true;
            }
        }

        return false;
    }
}
