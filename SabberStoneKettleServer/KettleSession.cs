﻿using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SabberStoneKettleServer
{
    class KettleSession
    {
        private Socket Client;
        private KettleAdapter Adapter;
        private Game Game;

        public KettleSession(Socket client)
        {
            Client = client;
            Adapter = new KettleAdapter(new NetworkStream(client));
            Adapter.OnCreateGame += OnCreateGame;
            Adapter.OnConcede += OnConcede;
            Adapter.OnChooseEntities += OnChooseEntities;
            Adapter.OnSendOption += OnSendOption;
        }

        public void Enter()
        {
            while (true)
            {
                Console.WriteLine("Handling next packet..");
                if (!Adapter.HandleNextPacket())
                {
                    Console.WriteLine("Kettle session ended.");
                    return;
                }
                Console.WriteLine("Done!");
            }
        }

        public void OnConcede(int concede)
        {

        }

        public void OnSendOption(KettleSendOption sendOption)
        {

        }

        public void OnChooseEntities(List<int> chooseEntities)
        {

        }

        public void OnCreateGame(KettleCreateGame createGame)
        {
            Console.WriteLine("creating game");
            Game = new Game(new GameConfig
                    {
                        StartPlayer = 1,
                        Player1HeroClass = CardClass.PRIEST,
                        Player2HeroClass = CardClass.HUNTER,
                        FillDecks = true
                    });

            // TODO: perhaps add kettlesession callbacks to game whenever something changes, so it can send packets through kettle?
            Game.StartGame();
            var answer = Game.PowerHistory.Last;
        }
    }
}
