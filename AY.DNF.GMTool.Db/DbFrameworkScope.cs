﻿using AY.DNF.GMTool.Db.DbModels.GMTool;
using SqlSugar;
using System;
using System.IO;
using System.Linq;
using TiaoTiaoCode.NLogger;

namespace AY.DNF.GMTool.Db
{
    public class DbFrameworkScope
    {
        #region 数据库

        static SqlSugarScope? _dTaiwan;
        static SqlSugarScope? _taiwanCain;
        static SqlSugarScope? _taiwanBillinig;
        static SqlSugarScope? _taiwanCain2nd;
        //static SqlSugarScope? _localDb;
        static SqlSugarScope? _gmToolDb;
        /// <summary>
        /// 用户信息库
        /// </summary>
        public static SqlSugarScope DTaiwan => _dTaiwan!;

        /// <summary>
        /// 角色信息库
        /// </summary>
        public static SqlSugarScope TaiwanCain => _taiwanCain!;

        /// <summary>
        /// 支付信息库
        /// D币 D点
        /// </summary>
        public static SqlSugarScope TaiwanBilling => _taiwanBillinig!;

        /// <summary>
        /// 邮件 背包
        /// </summary>
        public static SqlSugarScope TaiwanCain2nd => _taiwanCain2nd!;

        /// <summary>
        /// GM工具数据库
        /// </summary>
        public static SqlSugarScope GMToolDb => _gmToolDb!;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="port"></param>
        public static bool Init(string server, string userName, string pwd, int port)
        {
            try
            {
                var baseConn = $"Server={server};Port={port};Uid={userName};Pwd={pwd};Charset=utf8;AllowZeroDateTime=True;ConvertZeroDateTime=True;";
                _dTaiwan = new SqlSugarScope(new ConnectionConfig
                {
                    DbType = DbType.MySql,
                    ConfigId = "d_taiwan",
                    ConnectionString = $"{baseConn}Database=d_taiwan;",
                    IsAutoCloseConnection = true,
                });

                _taiwanCain = new SqlSugarScope(new ConnectionConfig
                {
                    DbType = DbType.MySql,
                    ConfigId = "taiwan_cain",
                    ConnectionString = $"{baseConn}Database=taiwan_cain;",
                    IsAutoCloseConnection = true,
                });

                _taiwanBillinig = new SqlSugarScope(new ConnectionConfig
                {
                    DbType = DbType.MySql,
                    ConfigId = "taiwan_billing",
                    ConnectionString = $"{baseConn}Database=taiwan_billing;",
                    IsAutoCloseConnection = true,
                });

                _taiwanCain2nd = new SqlSugarScope(new ConnectionConfig
                {
                    DbType = DbType.MySql,
                    ConfigId = "taiwan_cain_2nd",
                    ConnectionString = $"{baseConn}Database=taiwan_cain_2nd;",
                    IsAutoCloseConnection = true,
                });

                var sqliteDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalData");
                if (!Directory.Exists(sqliteDb))
                    Directory.CreateDirectory(sqliteDb);
                sqliteDb = Path.Combine(sqliteDb, "dnf.db");

                _gmToolDb = new SqlSugarScope(new ConnectionConfig
                {
                    DbType = DbType.Sqlite,
                    ConfigId = "gm_tool",
                    ConnectionString = $"DataSource={sqliteDb}",
                    IsAutoCloseConnection = true,
                });

                _dTaiwan.Ado.CheckConnection();
                _taiwanCain.Ado.CheckConnection();
                _taiwanBillinig.Ado.CheckConnection();
                _taiwanCain2nd.Ado.CheckConnection();

                if (!_gmToolDb.DbMaintenance.IsAnyTable("Equipments"))
                    _gmToolDb.CodeFirst.InitTables(typeof(Equipments));
                if (!_gmToolDb.DbMaintenance.IsAnyTable("Stackables"))
                    _gmToolDb.CodeFirst.InitTables(typeof(Stackables));
                if (!_gmToolDb.DbMaintenance.IsAnyTable("Dungeons"))
                    _gmToolDb.CodeFirst.InitTables(typeof(Dungeons));
                if (!_gmToolDb.DbMaintenance.IsAnyTable("JobTree"))
                    _gmToolDb.CodeFirst.InitTables(typeof(JobTree));
                if (!_gmToolDb.DbMaintenance.IsAnyTable("Quests"))
                    _gmToolDb.CodeFirst.InitTables(typeof(Quests));

                _gmToolDb.Ado.CheckConnection();

                CheckCharacInfoVIPColumn();

                return true;
            }
            catch (Exception ex)
            {
                TiaoTiaoNLogger.LogError(ex.Message);
                return false;
            }
        }

        public static void Disconnect()
        {
            DTaiwan.Close();
            TaiwanCain.Close();
            TaiwanBilling.Close();
            TaiwanCain2nd.Close();
            GMToolDb.Close();
        }

        /// <summary>
        /// 检查是否有VIP字段，有的库版本没有
        /// 没有VIP字段的自动补充
        /// </summary>
        static void CheckCharacInfoVIPColumn()
        {
            var cols = TaiwanCain.DbMaintenance.GetColumnInfosByTableName("charac_info");
            if (cols != null && cols.Count > 0)
            {

                var b = cols.Any(t => t.DbColumnName.ToUpper() == "VIP");
                if (b) return;

                TaiwanCain.DbMaintenance.AddColumn(
                    "charac_info",
                    new DbColumnInfo
                    {
                        DbColumnName = "VIP",
                        DataType = "varchar",
                        IsNullable = false,
                        Length = 255,
                    }
                    );
            }

            cols = DTaiwan.DbMaintenance.GetColumnInfosByTableName("accounts");
            if (cols != null && cols.Count > 0)
            {

                var b = cols.Any(t => t.DbColumnName.ToUpper() == "VIP");
                if (b) return;

                DTaiwan.DbMaintenance.AddColumn(
                    "accounts",
                    new DbColumnInfo
                    {
                        DbColumnName = "VIP",
                        DataType = "varchar",
                        IsNullable = false,
                        Length = 255,
                    }
                    );
            }
        }
    }
}
