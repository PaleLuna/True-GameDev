using UnityEngine;
using ConfigClasses.BuildingConfig;
using Buildings;

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
            int buildingCost = buildPrefab.buildingsConfig.levelCost;

            if (CheckBalance(buildingCost))
            {
                walletScript.SubFromCurrentBalance(buildingCost);
                placeForBuild.SetBuilding(Instantiate(buildPrefab, parentObjForBuildings));
            }
            else
            {
                Debug.Log("������������ ������!");
            }
        }

        /** ����� ��������� ���������.
         * @param TacticalPoint placeForBuild - �������, �� ������� ������������� �����.
         */
        public void UpgradeTower(TacticalPoint placeForBuild)
        {
            BuildingsConfig buildingConfig = placeForBuild.GetBuilding().GetNextLevel();
            if (buildingConfig)
            {
                int buildingUpgradeCost = buildingConfig.levelCost;

                if (CheckBalance(buildingUpgradeCost))
                {
                    placeForBuild.GetBuilding().SetNextLevel();
                    walletScript.SubFromCurrentBalance(buildingUpgradeCost);
                }
                else
                {
                    Debug.Log("������������ ������!");
                }
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