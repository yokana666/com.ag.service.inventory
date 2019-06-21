using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.Services.MaterialDistributionNoteService;
using Com.Danliris.Service.Inventory.Test.Helpers;
using Model = Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel;
using Com.Danliris.Service.Inventory.Test.DataUtils.MaterialDistributionNoteDataUtil;

namespace Com.Danliris.Service.Inventory.Test.Controllers.MaterialDistributionNote
{
    //[Collection("TestServerFixture Collection")]
    //public class MaterialDistributionNoteBasicTest : BasicControllerTest<InventoryDbContext, MaterialDistributionNoteService, Model.MaterialDistributionNote, MaterialDistributionNoteViewModel, MaterialDistributionNoteDataUtil>
    //{
    //    private static string URI = "v1/material-distribution-notes";
    //    private static List<string> CreateValidationAttributes = new List<string> { "Unit", "Type", "MaterialDistributionNoteItems" };
    //    private static List<string> UpdateValidationAttributes = new List<string> { "Unit", "Type", "MaterialDistributionNoteItems" };

    //    public MaterialDistributionNoteBasicTest(TestServerFixture fixture) : base(fixture, URI, CreateValidationAttributes, UpdateValidationAttributes)
    //    {
    //    }
    //}
}
