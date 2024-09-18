using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AIInputInstaller", menuName = "Installers/AIInputInstaller")]
public class AIInputInstaller : ScriptableObjectInstaller<AIInputInstaller>
{
    [SerializeField] private GameObject _input;
    public override void InstallBindings()
    {
        Container.Bind<AiInput>().FromComponentInNewPrefab(_input).AsTransient().Lazy();
    }
}