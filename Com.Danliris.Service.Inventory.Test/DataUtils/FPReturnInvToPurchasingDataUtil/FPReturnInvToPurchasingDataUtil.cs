namespace Com.Danliris.Service.Inventory.Test.DataUtils.FPReturnInvToPurchasingDataUtil
{
    //public class FPReturnInvToPurchasingDataUtil : BaseDataUtil<FPReturnInvToPurchasing, FPReturnInvToPurchasingFacade>, IEmptyData<FPReturnInvToPurchasingViewModel>
    //{
    //    //private readonly FpRegradingResultDataUtil.FpRegradingResultDataUtil fpRegradingResultDataUtil;
    //    private readonly FPReturnInvToPurchasingDetailDataUtil fpReturnInvToPurchasingDetailDataUtil;

    //    public FPReturnInvToPurchasingDataUtil(FPReturnInvToPurchasingFacade facade, FpRegradingResultDataUtil.FpRegradingResultDataUtil fpRegradingResultDataUtil, FPReturnInvToPurchasingDetailDataUtil fpReturnInvToPurchasingDetailDataUtil) : base(facade)
    //    {
    //        this.fpRegradingResultDataUtil = fpRegradingResultDataUtil;
    //        this.fpReturnInvToPurchasingDetailDataUtil = fpReturnInvToPurchasingDetailDataUtil;
    //    }

    //    public FPReturnInvToPurchasingViewModel GetEmptyData()
    //    {
    //        return new FPReturnInvToPurchasingViewModel
    //        {
    //            Unit = new UnitViewModel(),
    //            Supplier = new SupplierViewModel(),
    //            FPReturnInvToPurchasingDetails = new List<FPReturnInvToPurchasingDetailViewModel>() {  new FPReturnInvToPurchasingDetailViewModel() }
    //        };
    //    }

    //    public override FPReturnInvToPurchasing GetNewData()
    //    {
    //        Task<FpRegradingResultDocs> fpRegradingResultDocs = Task.Run(() => fpRegradingResultDataUtil.GetTestData());
    //        fpRegradingResultDocs.Wait();

    //        FPReturnInvToPurchasing TestData = new FPReturnInvToPurchasing
    //        {
    //            UnitName = fpRegradingResultDocs.Result.UnitName,
    //            SupplierId = fpRegradingResultDocs.Result.SupplierId,
    //            SupplierName = fpRegradingResultDocs.Result.SupplierName,
    //            SupplierCode = fpRegradingResultDocs.Result.SupplierCode,
    //            FPReturnInvToPurchasingDetails = fpReturnInvToPurchasingDetailDataUtil.GetNewData(fpRegradingResultDocs.Result)
    //        };

    //        return TestData;
    //    }


    //    public override async Task<FPReturnInvToPurchasing> GetTestData()
    //    {
    //        FPReturnInvToPurchasing Data = GetNewData();

    //        await this.Facade.Create(Data);
    //        return Data;
    //    }
    //}
}
