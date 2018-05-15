using Com.Danliris.Service.Inventory.Lib.Services.MaterialsRequestNoteServices;
using Com.Danliris.Service.Inventory.Test.DataUtils.MaterialRequestNoteDataUtil;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using model = Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;

namespace Com.Danliris.Service.Inventory.Test.Services.MaterialRequestNote
{
    class MaterialRequestNoteCustomTest
    {
        private readonly MaterialRequestNoteDataUtil dataUtil;
        private readonly MaterialsRequestNoteService service;
        public MaterialRequestNoteCustomTest(MaterialRequestNoteDataUtil dataUtil, MaterialsRequestNoteService materialsRequestNoteService)
        {
            this.dataUtil = dataUtil;
            this.service = materialsRequestNoteService;
        }

        [Fact]
        public async void Should_Success_Update_Data()
        {
            model.MaterialsRequestNote Data = await this.dataUtil.GetTestDataCustom();
            int AffectedRows = await this.service.UpdateModel(Data.Id, Data);

            Assert.True(AffectedRows > 0);
        }
        
    }


}
