using System;
using System.Collections.Generic;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.Extensions;
using DoubleDTeam.InputSystem.Base;
using UnityEngine;

namespace DoubleDTeam.InputSystem
{
    public class InputController : IModule
    {
        private readonly Dictionary<Type, InputMap> _maps = new();

        public InputMap CurrentMap { get; private set; }

        public void BindMap(InputMap map)
        {
            map.Initialize();
            map.Disable();

            _maps.TryAdd(map.GetType(), map);
        }

        public void EnableMap<TMap>() where TMap : InputMap
        {
            var mapType = typeof(TMap);

            if (_maps.ContainsKey(mapType) == false)
                return;

            var map = _maps[mapType];

            if (map == CurrentMap)
                return;

            if (CurrentMap != null)
                CurrentMap.Disable();

            CurrentMap = map;

            CurrentMap.Enable();

            Debug.Log($"{mapType.Name} map enabled".Color(Color.green));
        }

        public void DisableActiveMap()
        {
            if (CurrentMap == null)
                return;

            CurrentMap.Disable();
            CurrentMap = null;
        }

        public TMap GetMap<TMap>() where TMap : InputMap
        {
            var mapType = typeof(TMap);

            if (_maps.TryGetValue(mapType, out var map))
                return map as TMap;

            Debug.LogError($"Map {nameof(TMap)} does not exist");
            return null;
        }
    }
}