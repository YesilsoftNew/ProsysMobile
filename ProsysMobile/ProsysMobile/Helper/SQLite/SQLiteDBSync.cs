﻿using ProsysMobile.Models.CommonModels.SQLiteModels;
using WorkOrderProcessStatus = ProsysMobile.Models.CommonModels.SQLiteModels.WorkOrderProcessStatus;

namespace ProsysMobile.Helper.SQLite
{
    public class SQLiteDBSync : BaseSync
    {
        public SQLiteDBSync()
        {
            SQLiteCreateTable();
        }

        public void SQLiteCreateTable()
        {
            //Comment alınanlar değerlendirilip açılacak ya da silinecek- açılacak olanlar için service,endpoint vs. yazılacak
            Database.SQLConnection.CreateTable<User>();
            Database.SQLConnection.CreateTable<Permission>();
            Database.SQLConnection.CreateTable<CappedCasing>();
            Database.SQLConnection.CreateTable<CarBrand>();
            Database.SQLConnection.CreateTable<CarModel>();
            Database.SQLConnection.CreateTable<CurrencyType>();
            Database.SQLConnection.CreateTable<Flag>();
            Database.SQLConnection.CreateTable<Fleet>();
            Database.SQLConnection.CreateTable<FleetBranch>();
            Database.SQLConnection.CreateTable<FleetBranchCappedCasingMatch>();
            Database.SQLConnection.CreateTable<FleetBranchTIRActionMatch>();
            Database.SQLConnection.CreateTable<FleetBranchTireMatch>();
            Database.SQLConnection.CreateTable<FleetBranchVehicleType>();
            Database.SQLConnection.CreateTable<FleetBranchVehicleTypeMatch>();
            Database.SQLConnection.CreateTable<MobileSettings>();
            Database.SQLConnection.CreateTable<NotificationUser>();
            Database.SQLConnection.CreateTable<Parameter>();
            Database.SQLConnection.CreateTable<ServiceCenterWorkPool>();
            Database.SQLConnection.CreateTable<ServiceCenterWorkPoolHeader>();
            Database.SQLConnection.CreateTable<ServiceCenterWorkPoolMaterial>();
            Database.SQLConnection.CreateTable<StockAction>();
            Database.SQLConnection.CreateTable<StockCenterDefinition>();
            Database.SQLConnection.CreateTable<StockCenterTireProductRelation>();
            Database.SQLConnection.CreateTable<Tire>();
            Database.SQLConnection.CreateTable<TireAction>();
            Database.SQLConnection.CreateTable<TireActionWorkProcessDefRelation>();
            Database.SQLConnection.CreateTable<TireBrand>();
            Database.SQLConnection.CreateTable<WarrantyReason>();
            Database.SQLConnection.CreateTable<TireProduct>();
            Database.SQLConnection.CreateTable<UserNotificationType>();
            Database.SQLConnection.CreateTable<Vehicle>();
            Database.SQLConnection.CreateTable<VehicleFlag>();
            Database.SQLConnection.CreateTable<VehicleOdometer>();
            Database.SQLConnection.CreateTable<VehiclePositionAxleSummarys>();
            Database.SQLConnection.CreateTable<VehicleTire>();
            Database.SQLConnection.CreateTable<VehicleType>();
            Database.SQLConnection.CreateTable<VehicleTypeDetail>();
            Database.SQLConnection.CreateTable<VehicleVehicleTypeDetail>();
            Database.SQLConnection.CreateTable<DefaultSettings>();
            Database.SQLConnection.CreateTable<Positions>();
            Database.SQLConnection.CreateTable<RepairTypes>();
            Database.SQLConnection.CreateTable<StockActionTypes>();
            Database.SQLConnection.CreateTable<StockEntranceTypes>();
            Database.SQLConnection.CreateTable<TIRAction>();
            Database.SQLConnection.CreateTable<TireActionTypes>();
            Database.SQLConnection.CreateTable<TireAreas>();
            Database.SQLConnection.CreateTable<TireOffReasonNexts>();
            Database.SQLConnection.CreateTable<TireOffReasons>();
            Database.SQLConnection.CreateTable<TirePosition>();
            Database.SQLConnection.CreateTable<TireProductStates>();
            Database.SQLConnection.CreateTable<TypeOfVehicle>();
            Database.SQLConnection.CreateTable<WarrantyResults>();
            Database.SQLConnection.CreateTable<WorkOrderAction>();
            Database.SQLConnection.CreateTable<LocationDefination>();
            Database.SQLConnection.CreateTable<FleetBranchLocationRelation>();
            Database.SQLConnection.CreateTable<StockEntranceTireTypes>();
            Database.SQLConnection.CreateTable<UserTypeDefinitionRelation>();
            Database.SQLConnection.CreateTable<UnitTypeDefinition>();
            Database.SQLConnection.CreateTable<UnitDefinition>();
            Database.SQLConnection.CreateTable<ServiceProviderDefinition>();
            Database.SQLConnection.CreateTable<ServiceCenterDefinition>();
            Database.SQLConnection.CreateTable<ServiceDefinition>();
            Database.SQLConnection.CreateTable<AgreementDefinition>();
            Database.SQLConnection.CreateTable<AgreementUsers>();
            Database.SQLConnection.CreateTable<DeviceInfo>();
            Database.SQLConnection.CreateTable<FleetBranchTIR>();
            Database.SQLConnection.CreateTable<FleetBranchTire>();
            Database.SQLConnection.CreateTable<FleetBranchTireMapping>();
            Database.SQLConnection.CreateTable<Role>();
            Database.SQLConnection.CreateTable<RolePermission>();
            Database.SQLConnection.CreateTable<TireActionTIRAction>();
            Database.SQLConnection.CreateTable<TireApplication>();
            Database.SQLConnection.CreateTable<TireProductHistory>();
            Database.SQLConnection.CreateTable<UsersFleetBranchMatch>();
            Database.SQLConnection.CreateTable<WorkOrder>();
            Database.SQLConnection.CreateTable<WorkOrderAuditRule>();
            Database.SQLConnection.CreateTable<WorkOrderAuditRuleFleetBranch>();
            Database.SQLConnection.CreateTable<WorkOrderAuditRuleTireType>();
            Database.SQLConnection.CreateTable<WorkOrderAutoRules>();
            Database.SQLConnection.CreateTable<WorkOrderAutoRulesBranch>();
            Database.SQLConnection.CreateTable<WorkOrderAutoRulesDetail>();
            Database.SQLConnection.CreateTable<WorkOrderAutoRulesServiceDetail>();
            Database.SQLConnection.CreateTable<WorkOrderAutoRulesType>();
            Database.SQLConnection.CreateTable<WorkOrderDetail>();
            Database.SQLConnection.CreateTable<WorkOrderProcessApprove>();
            Database.SQLConnection.CreateTable<WorkOrderProcessCancelledReason>();
            Database.SQLConnection.CreateTable<WorkOrderProcessDetail>();
            Database.SQLConnection.CreateTable<WorkOrderProcessNTFUser>();
            Database.SQLConnection.CreateTable<WorkOrderProcessOpeningType>();
            Database.SQLConnection.CreateTable<WorkOrderProcessStatus>();
            Database.SQLConnection.CreateTable<WorkOrderService>();
            Database.SQLConnection.CreateTable<WorkOrderServiceDetail>();
            Database.SQLConnection.CreateTable<WorkOrderServiceRules>();
            Database.SQLConnection.CreateTable<WorkOrderServiceType>();
            Database.SQLConnection.CreateTable<WorkProcessCloseRules>();
            Database.SQLConnection.CreateTable<WorkProcessDetail>();
            Database.SQLConnection.CreateTable<WorkProcessPriority>();
            Database.SQLConnection.CreateTable<WorkProcessService>();
            Database.SQLConnection.CreateTable<WorkProcessServiceDetail>();
            Database.SQLConnection.CreateTable<WorkProcessServicePrice>();
            Database.SQLConnection.CreateTable<ApiTransaction>();
            Database.SQLConnection.CreateTable<MaterialBrandDefinition>();
            Database.SQLConnection.CreateTable<MaterialTypeDefinition>();
            Database.SQLConnection.CreateTable<MaterialDefinition>();
            Database.SQLConnection.CreateTable<ServiceDefinitionMaterial>();
            Database.SQLConnection.CreateTable<PreDefineds>();
            Database.SQLConnection.CreateTable<PreDefinedItems>();
            Database.SQLConnection.CreateTable<City>();

            Database.SQLConnection.CreateTable<WorkProcessDef>();
            Database.SQLConnection.CreateTable<WorkProcessFleetBranchRelation>();
            Database.SQLConnection.CreateTable<WorkProcessServiceRelation>();
            Database.SQLConnection.CreateTable<WorkOrderProcess>();
            Database.SQLConnection.CreateTable<WorkOrderProcessComment>();
            Database.SQLConnection.CreateTable<WorkOrderProcessServices>();    
        }
        public void SQLiteDropTableForLngTables()
        {
            Database.SQLConnection.DropTable<ServiceDefinition>();

            Database.SQLConnection.DropTable<Positions>();
            Database.SQLConnection.DropTable<RepairTypes>();
            Database.SQLConnection.DropTable<StockActionTypes>();
            Database.SQLConnection.DropTable<StockEntranceTireTypes>();
            Database.SQLConnection.DropTable<StockEntranceTypes>();
            Database.SQLConnection.DropTable<TIRAction>();
            Database.SQLConnection.DropTable<TireActionTypes>();
            Database.SQLConnection.DropTable<TireAreas>();
            Database.SQLConnection.DropTable<TireOffReasonNexts>();
            Database.SQLConnection.DropTable<TireOffReasons>();
            Database.SQLConnection.DropTable<TirePosition>();
            Database.SQLConnection.DropTable<TireProductStates>();
            Database.SQLConnection.DropTable<TypeOfVehicle>();
            Database.SQLConnection.DropTable<UnitTypeDefinition>();
            Database.SQLConnection.DropTable<WarrantyReason>();
            Database.SQLConnection.DropTable<WarrantyResults>();
            Database.SQLConnection.DropTable<WorkOrderAction>();
            Database.SQLConnection.DropTable<MaterialBrandDefinition>();
            Database.SQLConnection.DropTable<MaterialDefinition>();
            Database.SQLConnection.DropTable<MaterialTypeDefinition>();
            Database.SQLConnection.DropTable<UnitDefinition>();
        }

        public void SQLiteDropTableForFleetBased() // filo bazlı veri cekilen tabloların siliyorum
        {            
            #region MultiUserModels
            // data cekereken bunları tekrardan cekiyoruz o yüzden siliyorum
            Database.SQLConnection.DropTable<Fleet>();
            Database.SQLConnection.DropTable<Permission>();
            Database.SQLConnection.DropTable<ServiceProviderDefinition>();
            Database.SQLConnection.DropTable<ServiceCenterDefinition>();
            Database.SQLConnection.DropTable<StockCenterDefinition>();
            Database.SQLConnection.DropTable<LocationDefination>();
            Database.SQLConnection.DropTable<FleetBranchLocationRelation>();
            Database.SQLConnection.DropTable<ServiceDefinition>();
            Database.SQLConnection.DropTable<FleetBranch>();
            Database.SQLConnection.DropTable<UserTypeDefinitionRelation>();
            Database.SQLConnection.DropTable<ServiceDefinitionMaterial>();
            #endregion

            //Database.SQLConnection.DropTable<AgreementDefinition>();
            //Database.SQLConnection.DropTable<AgreementUsers>();
            //Database.SQLConnection.DropTable<ApiTransaction>();
            //Database.SQLConnection.DropTable<CappedCasing>();
            //Database.SQLConnection.DropTable<CarBrand>();
            //Database.SQLConnection.DropTable<CarModel>();
            //Database.SQLConnection.DropTable<City>(); 
            //Database.SQLConnection.DropTable<CurrencyType>();
            //Database.SQLConnection.DropTable<DefaultSettings>();
            //Database.SQLConnection.DropTable<DeviceInfo>();
            Database.SQLConnection.DropTable<Flag>();
            Database.SQLConnection.DropTable<FleetBranchCappedCasingMatch>();
            Database.SQLConnection.DropTable<FleetBranchTIR>();
            Database.SQLConnection.DropTable<FleetBranchTIRActionMatch>();
            Database.SQLConnection.DropTable<FleetBranchTire>();
            Database.SQLConnection.DropTable<FleetBranchTireMapping>();
            Database.SQLConnection.DropTable<FleetBranchTireMatch>();
            Database.SQLConnection.DropTable<FleetBranchVehicleType>();
            Database.SQLConnection.DropTable<FleetBranchVehicleTypeMatch>();            
            //Database.SQLConnection.CreateTable<Fleet>();
            //Database.SQLConnection.DropTable<MaterialBrandDefinition>();
            //Database.SQLConnection.DropTable<MaterialDefinition>();
            //Database.SQLConnection.DropTable<MaterialTypeDefinition>();
            //Database.SQLConnection.DropTable<MobileSettings>();
            //Database.SQLConnection.DropTable<NotificationUser>();
            Database.SQLConnection.DropTable<Parameter>();
            //Database.SQLConnection.DropTable<PreDefinedItems>();
            //Database.SQLConnection.DropTable<PreDefineds>();            
            //Database.SQLConnection.DropTable<Role>();
            //Database.SQLConnection.DropTable<RolePermission>();
            Database.SQLConnection.DropTable<ServiceCenterWorkPool>();
            Database.SQLConnection.DropTable<ServiceCenterWorkPoolHeader>();
            Database.SQLConnection.DropTable<ServiceCenterWorkPoolMaterial>();
            Database.SQLConnection.DropTable<StockAction>();
            Database.SQLConnection.DropTable<StockCenterTireProductRelation>();
            //Database.SQLConnection.DropTable<Tire>();
            Database.SQLConnection.DropTable<TireAction>();
            Database.SQLConnection.DropTable<TireActionTIRAction>();
            Database.SQLConnection.DropTable<TireActionWorkProcessDefRelation>();
            //Database.SQLConnection.DropTable<TireApplication>();
            //Database.SQLConnection.DropTable<TireBrand>();
            Database.SQLConnection.DropTable<TireProduct>();
            Database.SQLConnection.DropTable<TireProductHistory>();
            //Database.SQLConnection.DropTable<UnitDefinition>();
            //Database.SQLConnection.DropTable<UserNotificationType>();
            Database.SQLConnection.DropTable<UsersFleetBranchMatch>();
            Database.SQLConnection.DropTable<Vehicle>();
            Database.SQLConnection.DropTable<VehicleFlag>();
            Database.SQLConnection.DropTable<VehicleOdometer>();
            Database.SQLConnection.DropTable<VehiclePositionAxleSummarys>();
            Database.SQLConnection.DropTable<VehicleTire>();
            //Database.SQLConnection.DropTable<VehicleType>();
            //Database.SQLConnection.DropTable<VehicleTypeDetail>();
            Database.SQLConnection.DropTable<VehicleVehicleTypeDetail>();
            //Database.SQLConnection.DropTable<WarrantyReason>();
            //Database.SQLConnection.DropTable<RepairTypes>();
            //Database.SQLConnection.DropTable<StockActionTypes>();
            //Database.SQLConnection.DropTable<StockEntranceTypes>();
            //Database.SQLConnection.DropTable<TIRAction>();
            //Database.SQLConnection.DropTable<TireActionTypes>();
            //Database.SQLConnection.DropTable<TireAreas>();
            //Database.SQLConnection.DropTable<TireOffReasonNexts>();
            //Database.SQLConnection.DropTable<TireOffReasons>();
            //Database.SQLConnection.DropTable<TirePosition>();
            //Database.SQLConnection.DropTable<TireProductStates>();
            //Database.SQLConnection.DropTable<TypeOfVehicle>();
            //Database.SQLConnection.DropTable<WarrantyResults>();
            //Database.SQLConnection.DropTable<WorkOrderAction>();
            //Database.SQLConnection.DropTable<StockEntranceTireTypes>();
            //Database.SQLConnection.DropTable<UnitTypeDefinition>();
            //Database.SQLConnection.DropTable<Positions>();
            Database.SQLConnection.DropTable<WorkProcessFleetBranchRelation>();
        }
    }
}
