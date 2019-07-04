﻿namespace AltVStrefaRPServer.Models.Inventory.Items
{
    public abstract class BaseItem
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public int StackSize { get; protected set; }

        protected BaseItem(){}

        public BaseItem(string name, int stackSize)
        {
            Name = name;
            StackSize = stackSize;
        }

        public abstract bool UseItem(Character character);
    }
}
