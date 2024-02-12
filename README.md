# AY.DNF.GMTool

### 工具说明

### 更新版本

#### 注意

##### 超级功能

- SP充值、TP充值、QP充值，需要重启服务后才能生效
- VIP相关暂不实现，感觉没什么用

##### 邮件功能

-   查询数据信息来源Script.pvf数据导入 

##### Script.pvf

-  首次使用时进行游戏对应版本的Script.pvf导入

- 导入数据目前包括：装备、道具、职业、任务

- 导入后的数据会形成数据，后续邮件、任务管理等需要查询的数据均来自导入数据。 

##### 泡点功能

- 需要操持GM Tool开启，定时发放D币、D点。具体发放数量可设定

- 只支持分钟为间隔发放单位

### 更新说明

##### v1.0.20240212

    核心功能点基本都齐备，作为正式版进行发布

1. 增加pvf解析任务数据功能

2. 增加任务管理模块功能

3. pvf解析功能道职业、任务数据导入均在"其他"项中显示操作日志

4. 邮件、超级功能操作消息自动清除

5. 超级功能中增加清理时装、清理宠物功能

##### v0.0.20240204

1. pvf解析数据入库

2. pvf数据中文转为简体入库

3. 邮件查询功能改为查询pvf导入数据

##### v0.2.20240130

1. 登录信息表删除非必要字段，兼容85版本登录问题。其他版本暂未测

2. 增加数据库连接切换密码按钮

3. 邮件功能界面微调

4. 活动管理界面微调

##### v0.1.20240130

1. 邮件查询相关数据从Access库转移到Sqlite，尽可能兼容环境机无office等情况

2. 去掉Access数据库相关

3. 增加部分异常Log日志

4. 数据库空时间问题导致的程序异常修复兼容

##### v0.0.20240129

1. 增加活动管理功能

2. 修复部分发现的bug

##### v0.0.20240127

临时发布完整版，提供试用。