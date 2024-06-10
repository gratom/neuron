using System.Collections;
using System.Collections.Generic;
using Managers.Datas;
using UnityEngine;

namespace Boot.Main
{
    using Global;
    using Managers;
    using Tools;

    [Assert]
    public class Boot : MonoBehaviour
    {
#pragma warning disable
        [SerializeField] private BootSettings bootSetting;
#pragma warning restore

        #region Unity functions

        private void Awake()
        {
            ManagersCreating();
            StartCoroutine(Loading());
        }

        #endregion Unity functions

        private void ManagersCreating()
        {
            GameObject managerGameObject = new GameObject("Managers");
            List<BaseManager> baseManagers = new List<BaseManager>();
            DontDestroyOnLoad(managerGameObject);
            for (int i = 0; i < bootSetting.Managers.Count; i++)
            {
                baseManagers.Add(Instantiate(bootSetting.Managers[i], managerGameObject.transform));
                Services.AddManager(baseManagers[i]);
            }
        }

        private IEnumerator Loading()
        {
            yield return new WaitForSeconds(bootSetting.BootTime);
            SceneLoader.LoadScene(bootSetting.NextSceneIndex, () =>
            {
                Services.GetManager<MainManager>().EntryPoint();
            });
        }
    }
}
