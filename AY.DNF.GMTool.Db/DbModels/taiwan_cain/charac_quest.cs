using SqlSugar;
using System;
using System.Collections.Generic;

namespace AY.DNF.GMTool.Db.DbModels.taiwan_cain
{
	/// <summary>
	/// 
	/// </summary>
	[SugarTable("charac_quest", TableDescription = "")]
	public class CharacQuest
	{
		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "charac_no" , ColumnDataType = "int", IsPrimaryKey = true, DefaultValue = "0", ColumnDescription = "")]
		public int CharacNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_10" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest10 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_15" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest15 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_20" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest20 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_30" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest30 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_40" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest40 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_40_ext" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest40Ext { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_50" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest50 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_60" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest60 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_70" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest70 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_etc" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] QuestEtc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_1" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_1_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play1Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_2" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_2_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play2Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_3" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_3_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play3Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_4" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_4_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play4Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_5" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_5_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play5Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_6" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play6 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_6_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play6Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_7" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play7 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_7_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play7Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_8" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play8 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_8_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play8Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_9" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play9 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_9_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play9Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_10" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short Play10 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "play_10_trigger" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int Play10Trigger { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_50_ext" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest50Ext { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_60_ext" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest60Ext { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_etc_ext" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] QuestEtcExt { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "quest_60_ext_2nd" , ColumnDataType = "binary", DefaultValue = "                                                                ", ColumnDescription = "")]
		public byte[] Quest60Ext2nd { get; set; }

	}
}
