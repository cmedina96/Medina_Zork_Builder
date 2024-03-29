﻿using System;
using System.IO;
using System.ComponentModel;
using InventoryManager.Data;
using Newtonsoft.Json;

namespace InventoryManager.WinForms.ViewModels
{
    // Encapsulates a World
    public class WorldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Filename { get; set; }

        public BindingList<Player> Players { get; set; }

        public World World
        {
            set
            {
                if (mWorld != value)
                {
                    mWorld = value;
                    if (mWorld != null)
                    {
                        Players = new BindingList<Player>(mWorld.Players);
                    }
                    else
                    {
                        Players = new BindingList<Player>(Array.Empty<Player>());
                    }
                }
            }
        }
        public WorldViewModel(World world = null)
        {
            World = world;
        }
        public void SaveWorld()
        {
            if (string.IsNullOrEmpty(Filename))
            {
                throw new InvalidProgramException("Filename expected.");
            }

            JsonSerializer serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };
            using (StreamWriter streamWriter = new StreamWriter(Filename))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonWriter, mWorld);
            }
        }

        private World mWorld;
    }
}
