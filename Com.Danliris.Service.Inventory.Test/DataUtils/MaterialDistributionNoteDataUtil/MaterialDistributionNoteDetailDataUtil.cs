using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Test.DataUtils.IntegrationDataUtil;
using Com.Danliris.Service.Inventory.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.MaterialDistributionNoteDataUtil
{
    public class MaterialDistributionNoteDetailDataUtil
    {
        private HttpClientService client;

        public MaterialDistributionNoteDetailDataUtil(HttpClientService client)
        {
            this.client = client;
        }

        public List<MaterialDistributionNoteDetail> GetNewData(MaterialsRequestNote materialsRequestNote)
        {
            List<MaterialDistributionNoteDetail> list = new List<MaterialDistributionNoteDetail>();
            SupplierViewModel supplier = SupplierDataUtil.GetSupplier(client);

            foreach (MaterialsRequestNote_Item item in materialsRequestNote.MaterialsRequestNote_Items)
            {
                MaterialDistributionNoteDetail detail = new MaterialDistributionNoteDetail();
                detail.MaterialsRequestNoteItemId = item.Id;
                detail.ProductionOrderId = item.ProductionOrderId;
                detail.ProductionOrderNo = item.ProductionOrderNo;
                detail.ProductionOrderIsCompleted = item.ProductionOrderIsCompleted;
                detail.ProductId = item.ProductId;
                detail.ProductCode = item.ProductCode;
                detail.ProductName = item.ProductName;
                detail.Grade = item.Grade;
                detail.Quantity = 1;
                detail.MaterialRequestNoteItemLength = item.Length;
                detail.ReceivedLength = 1;
                detail.IsDisposition = false;
                detail.IsCompleted = false;
                detail.SupplierId = supplier._id;
                detail.SupplierCode = supplier.code;
                detail.SupplierName = supplier.name;

                list.Add(detail);
            }

            return list;
        }
    }
}
