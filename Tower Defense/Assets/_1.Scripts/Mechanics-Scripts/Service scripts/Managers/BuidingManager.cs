using Buildings;
using Buildings.Modules;
using ConfigClasses.BuildingConfig;
using UnityEngine;

namespace Managers
{
    /** ��������, ���������� �� �������������� � �����������. 
     * ��� �������������� �������� � ���� ������������� (�������), ���������.
     */

    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private WalletScript walletScript; /**< WalletScript variable. ������, ���������� �������� ������ ������. */
        [SerializeField] private Transform parentObjForBuildings; /**< Transform variable. ������������ ������ ��� ���� ����� ��������. */

        /** ����� ��� ������������� (��������) ����� ���������.
         * @param TacticalPoint placeForBuild - ������� ��� ����� ��������.
         * @param Building buildPrefab - ������ ����� ���������.
         */

        public void Build(TacticalPoint placeForBuild, Building buildPrefab)
        {
            BuildingCharacteristics buildingCharacteristic = buildPrefab.GetComponentInChildren<BuildingCharacteristics>();
            if (buildingCharacteristic == null)
                BuildWithoutCost(placeForBuild, buildPrefab);
            else
                BuildWithCost(placeForBuild, buildPrefab, buildingCharacteristic.GetCurrentLevel().levelCost);
        }

        private void BuildWithoutCost(TacticalPoint placeForBuild, Building buildPrefab)
        {
            placeForBuild.SetBuilding(Instantiate(buildPrefab, parentObjForBuildings));
        }
        private void BuildWithCost(TacticalPoint placeForBuild, Building buildPrefab, int buildingCost)
        {
            if (CheckBalance(buildingCost))
            {
                walletScript.SubFromCurrentBalance(buildingCost);
                placeForBuild.SetBuilding(Instantiate(buildPrefab, parentObjForBuildings));
            }
            else
                Debug.Log("������������ ������!");
        }

        /** ����� ��������� ���������.
         * @param TacticalPoint placeForBuild - �������, �� ������� ������������� �����.
         */
        public void UpgradeTower(TacticalPoint placeForBuild)
        {
            BuildingCharacteristics buildingCharacteristic = placeForBuild.GetBuilding().GetComponentInChildren<BuildingCharacteristics>();
            if (buildingCharacteristic == null) return;

            int buildingUpgradeCost = buildingCharacteristic.GetNextLevel().levelCost;

            if (CheckBalance(buildingUpgradeCost))
            {
                buildingCharacteristic.SetNextLevel();
                walletScript.SubFromCurrentBalance(buildingUpgradeCost);
            }
            else
            {
                Debug.Log("������������ ������!");
            }
        }

        /** ����� ������� ���������.
         * @param TacticalPoint placeForBuild - �������, �� ������� ������������� �����.
         */
        public void SellBuild(TacticalPoint placeForBuild)
        {
            BuildingsConfig buildingConfig = placeForBuild.GetBuilding().buildingsConfig;
            int buildingSellCost = buildingConfig.sellCost;

            walletScript.AddToCurrentBalace(buildingSellCost);
            placeForBuild.DescructBuilding();
        }
        /** ����� �������� ����������� ����� � ��������.
         * @param int buildingCost - �����, ������� ���������� �������.
         */
        private bool CheckBalance(int buildingCost)
        {
            int currentBalance = walletScript.currentBalance;

            return currentBalance >= buildingCost;
        }
    }
}