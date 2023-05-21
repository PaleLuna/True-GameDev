using Buildings;
using Buildings.Modules;
using ConfigClasses.ConfigBuildings;
using UnityEngine;

namespace Managers
{
    /** ��������, ���������� �� �������������� � �����������. 
     * ��� �������������� �������� � ���� ������������� (�������), ���������.
     */

    public class BuildingManager : Singleton<BuildingManager>
    {
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
            placeForBuild.SetBuilding(buildPrefab);
        }
        private void BuildWithCost(TacticalPoint placeForBuild, Building buildPrefab, int buildingCost)
        {
            if (CheckBalance(buildingCost))
            {
                WalletScript.instance.SubFromCurrentBalance(buildingCost);
                placeForBuild.SetBuilding(buildPrefab);
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
            if (buildingCharacteristic == null || buildingCharacteristic.GetNextLevel() == null) 
                return;

            int buildingUpgradeCost = buildingCharacteristic.GetNextLevel().levelCost;

            if (CheckBalance(buildingUpgradeCost))
            {
                buildingCharacteristic.SetNextLevel();
                WalletScript.instance.SubFromCurrentBalance(buildingUpgradeCost);
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
            BuildingConfig buildingConfig = placeForBuild.GetBuilding().buildingsConfig;
            int buildingSellCost = buildingConfig.sellCost;

            WalletScript.instance.AddToCurrentBalace(buildingSellCost);
            placeForBuild.DescructBuilding();
        }
        /** ����� �������� ����������� ����� � ��������.
         * @param int buildingCost - �����, ������� ���������� �������.
         */
        private bool CheckBalance(int buildingCost)
        {
            int currentBalance = WalletScript.instance.currentBalance;

            return currentBalance >= buildingCost;
        }
    }
}