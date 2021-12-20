using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.CoreGame.Enums;
using ConsoleApp1.CoreGame.Interfaces;
using ConsoleApp1.Database;
using ConsoleApp1.Network;
using ConsoleApp1.Network.Entity;

namespace ConsoleApp1.Repository
{
    /// <summary>
    ///     Класс-репозиторий. Имплементирует IRepository.
    /// </summary>
    public class RepositoryImpl: IRepository, IDisposable
    {
        private PostgresDatabaseORM _postgresDatabase;
        private INetworkService _networkService;

        /// <summary>
        ///     Конструктор репозитория. Принимает базу данных и сервис API.
        /// </summary>
        /// <param name="database">
        ///     Объект PostgresDatabaseORM. Должен быть проинициализирован и настроен заранее,
        ///     репозиторий этим не занимается.
        /// </param>
        /// <param name="networkService">
        ///     Имплементация интерфейса INetworkService. Должна быть проинициализирована и настроена заранее,
        ///     репозиторий этим не занимается.
        /// </param>
        public RepositoryImpl(PostgresDatabaseORM database, INetworkService networkService)
        {
            _postgresDatabase = database;
            _networkService = networkService;
        }
        
        public void SaveWorldBehavior(WorldBehavior worldBehavior)
        {
            _postgresDatabase.SaveWorldBehavior(worldBehavior.ToEntity());
        }

        public WorldBehavior GetWorldBehaviorByName(string name)
        {
            return _postgresDatabase.GetWorldBehaviorByName(name).ToDomain();
        }
        
        public List<WorldBehavior> GetAllWorldBehaviors()
        {
            return _postgresDatabase.GetAllWorldBehaviors().Select(it => it.ToDomain()).ToList();
        }

        public void UpdateWorldBehavior(WorldBehavior worldBehavior)
        {
            _postgresDatabase.EditWorldBehavior(worldBehavior.Name, Converters.convertListToJson(worldBehavior.FoodCoords));
        }

        public void DeleteWorldBehavior(string name)
        {
            _postgresDatabase.DeleteWorldBehavior(name);
        }

        public (Actions, Directions) GetWormActionFromAPI(string wormName, IWorldInfoProvider infoProvider)
        {
            InfoForServer infoForServer = new InfoForServer(infoProvider);
            var response = _networkService.GetWormAction(wormName, infoForServer);
            var infoFromServer = response.Result;

            Actions action = Actions.None;
            Directions direction = Directions.None;

            if (infoFromServer.Action.Split)
            {
                action = Actions.Budding;
            }
            else
            {
                action = Actions.Move;
            }

            switch (infoFromServer.Action.Direction)
            {
                case "Up":
                {
                    direction = Directions.Top;
                    break;
                }
                case "Down":
                {
                    direction = Directions.Bottom;
                    break;
                }
                case "Left":
                {
                    direction = Directions.Left;
                    break;
                }
                case "Right":
                {
                    direction = Directions.Right;
                    break;
                }
                case "None":
                {
                    direction = Directions.None;
                    action = Actions.None;
                    break;
                }
            }

            return (action, direction);
        }

        public void Dispose()
        {
            _postgresDatabase.Dispose();
        }
    }
}