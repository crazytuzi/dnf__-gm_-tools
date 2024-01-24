using SqlSugar;
using System;
using System.Collections.Generic;

namespace AY.DNF.GMTool.Db.DbModels.taiwan_cain_2nd
{
	/// <summary>
	/// 
	/// </summary>
	[SugarTable("skill_fair_pvp", TableDescription = "")]
	public class SkillFairPvp
	{
		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "charac_no" , ColumnDataType = "int", IsPrimaryKey = true, DefaultValue = "0", ColumnDescription = "")]
		public int CharacNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "remain_sp" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int RemainSp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "skill_slot" , ColumnDataType = "blob", IsNullable = true, ColumnDescription = "")]
		public byte[]? SkillSlot { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "sp_garbage" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int SpGarbage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "used_sp" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int UsedSp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "skill_slot_lethe" , ColumnDataType = "blob", IsNullable = true, ColumnDescription = "")]
		public byte[]? SkillSlotLethe { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "lethe_flag" , ColumnDataType = "tinyint", DefaultValue = "0", ColumnDescription = "")]
		public long LetheFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "remain_sp_2nd" , ColumnDataType = "int", DefaultValue = "0", ColumnDescription = "")]
		public int RemainSp2nd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "skill_slot_2nd" , ColumnDataType = "blob", IsNullable = true, ColumnDescription = "")]
		public byte[]? SkillSlot2nd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "skill_slot_lethe_2nd" , ColumnDataType = "blob", IsNullable = true, ColumnDescription = "")]
		public byte[]? SkillSlotLethe2nd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "lethe_flag_2nd" , ColumnDataType = "tinyint", DefaultValue = "0", ColumnDescription = "")]
		public long LetheFlag2nd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "remain_sfp_1st" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short RemainSfp1st { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "remain_sfp_2nd" , ColumnDataType = "smallint", DefaultValue = "0", ColumnDescription = "")]
		public short RemainSfp2nd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "skill_command" , ColumnDataType = "blob", IsNullable = true, ColumnDescription = "")]
		public byte[]? SkillCommand { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "script_version" , ColumnDataType = "tinyint", DefaultValue = "0", ColumnDescription = "")]
		public long ScriptVersion { get; set; }

	}
}
