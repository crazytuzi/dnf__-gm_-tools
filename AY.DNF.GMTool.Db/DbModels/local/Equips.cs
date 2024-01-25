﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace AY.DNF.GMTool.Db.DbModels.local
{
    [SugarTable("Equips")]
    public class Equips
    {
        public string ItemName { get; set; }
        public string ItemId { get; set; }
        public int Sort { get; set; }
    }
}
