﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltVStrefaRPServer.Services.Housing;

namespace AltVStrefaRPServer.Models.Houses
{
    public class Interior : IInterior
    {
        /// <summary>
        /// Database id of the interior
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Name of the interior
        /// </summary>
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        
        public float EnterX { get; set; }
        public float EnterY { get; set; }
        public float EnterZ { get; set; }

        /// <summary>
        /// Collection of flats using this interior
        /// </summary>
        public ICollection<Flat> Flats { get; private set; } = new List<Flat>();
        
        public IColShape Colshape { get; set; }

        public Interior(string name, float x, float y, float z, float enterX, float enterY, float enterZ)
        {
            Name = name;
            X = x;
            Y = y;
            Z = z;
            EnterX = enterX;
            EnterY = enterY;
            EnterZ = enterZ;

            Colshape = Alt.CreateColShapeCylinder(GetPosition(), 1f, 1f);

            // Interior exit colshape 
            Alt.OnColShape += (shape, entity, state) =>
            {
                if (shape != Colshape) return;
                if (!(entity is IStrefaPlayer player)) return;

                player.Emit("showInteriorExitMenu", state);
            };
            
            // Create marker where player can access house inventory
        }
        
        /// <summary>
        /// Gets position of the interior exit
        /// </summary>
        /// <returns></returns>
        public Position GetPosition() => new Position(X, Y, Z);

        /// <summary>
        /// Gets position where will the player teleport
        /// </summary>
        /// <returns></returns>
        public Position GetEnterPosition() => new Position(X, Y, Z);

        public async Task DeleteInteriorAsync(IInteriorDatabaseService interiorDatabaseService)
        {
            Colshape.Remove();
            await interiorDatabaseService.RemoveInteriorAsync(this);
        }
    }
}