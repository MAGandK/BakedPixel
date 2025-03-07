using Constants;
using Services.Inventory;
using Services.Storage;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private InventoryConfig _inventoryConfig;
        public override void InstallBindings()
        {
            BindStorage();
            BindInventory();
        }

        private void BindInventory()
        {
            Container.Bind<InventoryConfig>().FromInstance(_inventoryConfig).AsSingle();
            Container.Bind(typeof(IInventoryService), typeof(IInitializable)).To<InventoryService>().AsSingle();
        }

        private void BindStorage()
        {
            Container.Bind<IStorageData>().To<InventoryData>().AsSingle().WithArguments(StorageDataNames.INVENTORY_DATA_KEY);
            
            Container.Bind(typeof(IStorageService), typeof(IInitializable)).To<StorageService>()
                .AsSingle()
                .NonLazy();
        }
    }
}