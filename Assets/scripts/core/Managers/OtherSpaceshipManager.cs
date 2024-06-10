using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Global;
using Global.Component.Perceptron;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.Datas
{
    public class OtherSpaceshipManager : BaseManager
    {
        public override Type ManagerType => typeof(OtherSpaceshipManager);
        public float GeneticValue => geneticValue;
        public float KillBonus => killBonus;
        public double BasicKill => basicKill;

        [Range(0.1f, 5)][SerializeField] private float killBonus = 1;
        [Range(10, 500)][SerializeField] private float basicKill = 100;
        [SerializeField] private AnimationCurve powerToCost;
        [Range(0.1f, 20)][SerializeField] private float laserCost;
        [Range(0.1f, 2)][SerializeField] private float aggressive;


        [SerializeField] private SpaceShipControl otherSpaceShipPrefab;

        [SerializeField] private int count;
        [Range(0f, 1)][SerializeField] private float geneticValue;

        [Range(0.1f, 1)][SerializeField] private float selection = 0.75f;
        [SerializeField] private bool isGeneticActive;
        [Range(5, 60)][SerializeField] private float geneticTimer;

        [SerializeField] private Text maxText;

        private List<SpaceShipControl> ships = new List<SpaceShipControl>();

        private SpaceShipControl player;

        private void Start()
        {
            StartCoroutine(LifeCoroutine());
        }

        private IEnumerator LifeCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(geneticTimer);
                if (isGeneticActive)
                {
                    RespawnAll();
                }
            }
        }

        private void FixedUpdate()
        {
            if (ships.Count > 0)
            {
                float max = ships.Max(x => x.Money);
                maxText.text = "max : " + max.ToString("0.0");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                RespawnAll();
            }
        }

        private void RespawnAll()
        {
            float max = ships.Max(x => x.Money);

            int count = 0;
            foreach (SpaceShipControl shipControl in ships.Where(x => x.Money < x.SavedMoney))
            {
                count++;
                RespawnMe(shipControl);
            }
            Debug.Log("respawnAll(" + count + "), max:" + max.ToString("0.0") + "|  " + ships.First(x => Mathf.Approximately(x.Money, max)).name);

            foreach (SpaceShipControl shipControl in ships)
            {
                shipControl.Money = shipControl.SavedMoney;
                shipControl.laserCost = laserCost;
            }
        }

        public void StartWork()
        {
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Vector2Int quad = new Vector2Int(i, j);
                    ships.Add(Instantiate(otherSpaceShipPrefab, GetRndPos(quad), otherSpaceShipPrefab.transform.rotation));
                    ships[index].quad = quad;
                    ships[index].name = "spaceShip" + index;
                    index++;
                }
            }
        }

        public void RespawnMe(SpaceShipControl spaceShipControl)
        {
            spaceShipControl.transform.position = GetRndPos(spaceShipControl.quad);
            if (spaceShipControl.TryGetComponent<ComputerControl>(out ComputerControl control))
            {
                control.Refresh();
            }
            spaceShipControl.WasRespawned();
        }

        public Vector3 GetRndPos()
        {
            return Services.GetManager<AsteroidManager>().GetRndPos();
        }

        public Vector3 GetRndPos(Vector2Int quad)
        {
            return Services.GetManager<AsteroidManager>().GetRndPos(quad);
        }

        public List<Brain> GetBests(SpaceShipControl shipControl)
        {
            float max = ships.Max(x => x.Money);
            maxText.text = "max : " + max.ToString("0.0");
            double average = ships.Average(x => x.Money);
            double diff = max - average;
            List<Brain> bests = ships.Where(x => x.Money > max - diff * selection && shipControl.hit * x.hit >= 0).Select(x => x.GetComponent<Brain>()).ToList();
            return bests;
        }

        public Brain GetBest(SpaceShipControl shipControl)
        {
            SpaceShipControl[] selected = ships.Where(x => x.hit * shipControl.hit * x.hit >= 0).ToArray();
            float max = selected.Any() ? selected.Max(x => x.Money) : ships.Max(x => x.Money);
            maxText.text = "max : " + max;
            Brain best = ships.First(x => Mathf.Approximately(x.Money, max)).GetComponent<Brain>();
            return best;
        }

        public Vector2 GetNearestShipPos(Vector2 point)
        {
            float nearest = Vector2.Distance(point, new Vector2(player.transform.position.x, player.transform.position.z)) * aggressive;
            int nearestIndex = -1;
            for (int i = 0; i < ships.Count; i++)
            {
                float distance = Vector2.Distance(point, new Vector2(ships[i].transform.position.x, ships[i].transform.position.z));
                if (distance > 0.001f)
                {
                    if (nearest > distance)
                    {
                        nearest = distance;
                        nearestIndex = i;
                    }
                }
            }
            if (nearestIndex == -1)
            {
                return new Vector2(player.transform.position.x, player.transform.position.z);
            }
            return new Vector2(ships[nearestIndex].transform.position.x, ships[nearestIndex].transform.position.z);
        }

        public float PowerToCost(float abs)
        {
            return powerToCost.Evaluate(abs);
        }

        public void SetPlayer(SpaceShipControl playerSpaceShipControl)
        {
            player = playerSpaceShipControl;
        }
    }
}
