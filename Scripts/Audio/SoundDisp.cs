using Godot;
using System;
using MySystems;
using System.Collections.Generic;

namespace Audio
{
    public class SoundDisp : Node
    {
        private SoundSystem _soundSys;
        //aquí metermos una lista con varios sounds y demas
        private List<SoundPlayerCustom> _unusedPlayers;
        private List<SoundPlayerCustom> _usingPlayers;

        private const int DEF_PLAYERS = 4;

        public override void _EnterTree()
        {
            base._EnterTree();
            System_Manager.GetInstance(this).TryGetSystem<SoundSystem>(out _soundSys, true);
            GD.Print(_soundSys);
            _unusedPlayers = new List<SoundPlayerCustom>();
            _usingPlayers = new List<SoundPlayerCustom>();
            this.PopulatePlayers();
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            for (int i = 0; i < _unusedPlayers.Count; i++){
                _unusedPlayers[i].OnFinishSound -= this.OnStopSound;                
            }

            for (int i = 0; i < _usingPlayers.Count; i++){
                _usingPlayers[i].OnFinishSound -= this.OnStopSound;
            }
            _unusedPlayers.Clear();
            _usingPlayers.Clear();
            _soundSys = null;
        }

        private void PopulatePlayers(){
            for (int i = 0; i < DEF_PLAYERS; i++){
                _unusedPlayers.Add(this.SpamPlayer());
            }
        }

        private SoundPlayerCustom SpamPlayer(){
            SoundPlayerCustom player = new SoundPlayerCustom();
            base.AddChild(player);
            player.Name = "Player_" + (_unusedPlayers.Count + _usingPlayers.Count);

            // TODO: esto habrá que cambiarlo con una const en algún sitio...
            player.Bus = "Effects";
            player.OnFinishSound += this.OnStopSound;
            return player;
        }

        private void OnStopSound(in SoundPlayerCustom sPlayer){
            for (int i = 0; i < _usingPlayers.Count; i++){
                if(_usingPlayers[i] == sPlayer){
                    _usingPlayers.RemoveAt(i);
                    _unusedPlayers.Add(sPlayer);
                    Messages.Print("Holaaaa, hemos encontrado lo que buscabamos...");
                    return;
                }
            }
        }

        public void PlaySound(in AudioStream sound){
            SoundPlayerCustom sPlayer;

            if(_unusedPlayers.Count > 0){
                sPlayer = _unusedPlayers[0];
                _unusedPlayers.RemoveAt(0);
                
            }else{
                sPlayer = this.SpamPlayer();
            }

            _usingPlayers.Add(sPlayer);
            sPlayer.Stream = sound;
            sPlayer.Play();
        }

        

    }
}
