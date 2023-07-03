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
            //Database.SQLConnection.CreateTable<User>();
            //Database.SQLConnection.CreateTable<Permission>();  
        }
        public void SQLiteDropTableForLngTables()
        {
            //Database.SQLConnection.DropTable<ServiceDefinition>();
            //Database.SQLConnection.DropTable<Positions>();
            //Database.SQLConnection.DropTable<RepairTypes>();
        }

        public void SQLiteDropTableForFleetBased() // filo bazlı veri cekilen tabloların siliyorum
        {            
            #region MultiUserModels
            // data cekereken bunları tekrardan cekiyoruz o yüzden siliyorum
            //Database.SQLConnection.DropTable<Fleet>();
            //Database.SQLConnection.DropTable<Permission>();
            //Database.SQLConnection.DropTable<ServiceProviderDefinition>();
            //Database.SQLConnection.DropTable<ServiceCenterDefinition>();
            //Database.SQLConnection.DropTable<StockCenterDefinition>();
            //Database.SQLConnection.DropTable<LocationDefination>();
            //Database.SQLConnection.DropTable<FleetBranchLocationRelation>();
            //Database.SQLConnection.DropTable<ServiceDefinition>();
            //Database.SQLConnection.DropTable<FleetBranch>();
            //Database.SQLConnection.DropTable<UserTypeDefinitionRelation>();
            //Database.SQLConnection.DropTable<ServiceDefinitionMaterial>();
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
            //Database.SQLConnection.DropTable<Flag>();
            //Database.SQLConnection.DropTable<FleetBranchCappedCasingMatch>();
            //Database.SQLConnection.DropTable<FleetBranchTIR>();
            //Database.SQLConnection.DropTable<FleetBranchTIRActionMatch>();
            //Database.SQLConnection.DropTable<FleetBranchTire>();
            //Database.SQLConnection.DropTable<FleetBranchTireMapping>();
            //Database.SQLConnection.DropTable<FleetBranchTireMatch>();
            //Database.SQLConnection.DropTable<FleetBranchVehicleType>();
            //Database.SQLConnection.DropTable<FleetBranchVehicleTypeMatch>();            
            //Database.SQLConnection.CreateTable<Fleet>();
            //Database.SQLConnection.DropTable<MaterialBrandDefinition>();
            //Database.SQLConnection.DropTable<MaterialDefinition>();
            //Database.SQLConnection.DropTable<MaterialTypeDefinition>();
            //Database.SQLConnection.DropTable<MobileSettings>();
            //Database.SQLConnection.DropTable<NotificationUser>();
            //Database.SQLConnection.DropTable<Parameter>();
            //Database.SQLConnection.DropTable<PreDefinedItems>();
            //Database.SQLConnection.DropTable<PreDefineds>();            
            //Database.SQLConnection.DropTable<Role>();
            //Database.SQLConnection.DropTable<RolePermission>();
            //Database.SQLConnection.DropTable<ServiceCenterWorkPool>();
            //Database.SQLConnection.DropTable<ServiceCenterWorkPoolHeader>();
            //Database.SQLConnection.DropTable<ServiceCenterWorkPoolMaterial>();
            //Database.SQLConnection.DropTable<StockAction>();
            //Database.SQLConnection.DropTable<StockCenterTireProductRelation>();
            //Database.SQLConnection.DropTable<Tire>();
            //Database.SQLConnection.DropTable<TireAction>();
            //Database.SQLConnection.DropTable<TireActionTIRAction>();
            //Database.SQLConnection.DropTable<TireActionWorkProcessDefRelation>();
            //Database.SQLConnection.DropTable<TireApplication>();
            //Database.SQLConnection.DropTable<TireBrand>();
            //Database.SQLConnection.DropTable<TireProduct>();
            //Database.SQLConnection.DropTable<TireProductHistory>();
            //Database.SQLConnection.DropTable<UnitDefinition>();
            //Database.SQLConnection.DropTable<UserNotificationType>();
            //Database.SQLConnection.DropTable<UsersFleetBranchMatch>();
            //Database.SQLConnection.DropTable<Vehicle>();
            //Database.SQLConnection.DropTable<VehicleFlag>();
            //Database.SQLConnection.DropTable<VehicleOdometer>();
            //Database.SQLConnection.DropTable<VehiclePositionAxleSummarys>();
            //Database.SQLConnection.DropTable<VehicleTire>();
            //Database.SQLConnection.DropTable<VehicleType>();
            //Database.SQLConnection.DropTable<VehicleTypeDetail>();
            //Database.SQLConnection.DropTable<VehicleVehicleTypeDetail>();
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
            //Database.SQLConnection.DropTable<WorkProcessFleetBranchRelation>();
        }
    }
}
