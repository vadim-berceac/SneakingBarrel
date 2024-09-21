using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "StatesInstaller", menuName = "Installers/StatesInstaller")]
public class StatesInstaller : ScriptableObjectInstaller<StatesInstaller>
{
    [SerializeField] private IdleState _idle;
    [SerializeField] private MoveState _move;  

    public override void InstallBindings()
    {
        Container.Bind<IdleState>().FromNewScriptableObject(_idle).AsSingle().NonLazy();
        Container.Bind<MoveState>().FromNewScriptableObject(_move).AsSingle().NonLazy();
    }
}