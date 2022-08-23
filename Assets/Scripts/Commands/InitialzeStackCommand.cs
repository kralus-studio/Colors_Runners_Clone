﻿using UnityEngine;
using Managers;
using Signals;


namespace Commands
{
    public class InitialzeStackCommand
    {
        #region Self Variables

        #region Private Variables

        private StackManager _manager;
        private GameObject _collectable;
        #endregion
        #endregion

        public InitialzeStackCommand(GameObject collectable,StackManager Manager)
        {
            _collectable = collectable;
            _manager = Manager;
        }
        public void Execute(int count)
        {
            for (int i = 1; i < count/*_manager.StackData.InitialStackItem*/; i++)
            {
                GameObject obj = Object.Instantiate(_collectable);
                _manager.ItemAddOnStack.Execute(obj);
            }
            // _manager.StackValueUpdateCommand.Execute();
        }
    }
}