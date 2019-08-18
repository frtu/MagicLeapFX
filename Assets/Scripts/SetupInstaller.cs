using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SetupInstaller : MonoInstaller
{
    public override void InstallBindings() {
        // Container.Bind<T>().AsTransient() : Prototype
        // Container.Bind<T>().AsSingle() : Singleton
        // Container.Bind<T>().AsSingle().NonLazy() : By default LazyInit
        
        Container.Bind<DiagnosticsManager>().AsSingle();
        Container.Bind<UIManager>().AsSingle();
    }
}
