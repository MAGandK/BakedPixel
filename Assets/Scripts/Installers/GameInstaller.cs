using Managers;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private DiContainer _diContainer;

        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            // Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            // Container.Bind<LevelPrefabManager>().FromComponentInHierarchy().AsSingle();
            // Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();
            // Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
        }
    }
}