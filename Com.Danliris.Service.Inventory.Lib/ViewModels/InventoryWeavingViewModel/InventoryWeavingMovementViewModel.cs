﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryWeavingViewModel
{
    public class InventoryWeavingMovementViewModel
    {
        public string ProductOrderName { get; set; }
        public string BonNo { get; set; }
        public string ReferenceNo { get; set; }
        public string Construction { get; set; }
        public string Grade { get; set; }
        public string Piece { get; set; }

        /*construction */
        public string MaterialName { get; set; }
        public string WovenType { get; set; }
        public string Yarn1 { get; set; }
        public string Yarn2 { get; set; }
        public string YarnType1 { get; set; }
        public string YarnType2 { get; set; }
        public string YarnOrigin1 { get; set; }
        public string YarnOrigin2 { get; set; }
        public string Width { get; set; }

        public string UomUnit { get; set; }
        public int UomId { get; set; }
        public double Quantity { get; set; }
        public double QuantityPiece { get; set; }
        public string ProductRemark { get; set; }
        public int InventoryWeavingDocumentId { get; set; }
        public int InventoryWeavingDocumentItemId { get; set; }
        public string Type { get; set; }
    }
}
