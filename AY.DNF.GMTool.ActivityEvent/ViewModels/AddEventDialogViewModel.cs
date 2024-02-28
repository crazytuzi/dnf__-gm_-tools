using AY.DNF.GMTool.Db.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AY.DNF.GMTool.ActivityEvent.ViewModels
{
    internal class AddEventDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "小白添加活动";

        #region 活动信息

        private List<SimpleEventModel> _eventList = new List<SimpleEventModel>();

        public List<SimpleEventModel> EventList
        {
            get { return _eventList; }
            set { SetProperty(ref _eventList, value); }
        }

        private long _eventId;

        private string? _p1;

        public string? P1
        {
            get { return _p1; }
            set { SetProperty(ref _p1, value); }
        }

        private string? _p2;

        public string? P2
        {
            get { return _p2; }
            set { SetProperty(ref _p2, value); }
        }

        private string? _p1Desc;

        public string? P1Desc
        {
            get { return _p1Desc; }
            set { SetProperty(ref _p1Desc, value); }
        }

        private string? _p2Desc;

        public string? P2Desc
        {
            get { return _p2Desc; }
            set { SetProperty(ref _p2Desc, value); }
        }

        private SimpleEventModel _selectedEvent;

        public SimpleEventModel SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                SetProperty(ref _selectedEvent, value);

                _eventId = long.Parse(value.EventId);
                P1 = value.DP1;
                P2 = value.DP2;
                P1Desc = value.P1Desc;
                P2Desc = value.P2Desc;
            }
        }

        private string? _msg;

        public string? Msg
        {
            get { return _msg; }
            set
            {
                SetProperty(ref _msg, value);
                Task.Run(() =>
                {
                    Task.Delay(2000);
                    Msg = null;
                });
            }
        }

        #endregion

        ICommand? _addEventCommand;

        public ICommand AddEventCommand => _addEventCommand ??= new DelegateCommand(DoAddEventCommand);

        public event Action<IDialogResult>? RequestClose;

        public AddEventDialogViewModel()
        {
            EventList.Add(new SimpleEventModel { EventId = "1", Title = "无限疲劳活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "2", Title = "疲劳翻倍活动", P1Desc = "填写100的倍数", DP1 = "100", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "3", Title = "经验翻倍活动", P1Desc = "填写100的倍数", DP1 = "100", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "5", Title = "创建角色赠送复活币", P1Desc = "填写赠送的个数", DP1 = "50", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "7", Title = "装备掉落率翻倍活动", P1Desc = "填写100的倍数", DP1 = "100", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "8", Title = "网咖燃烧时间", P1Desc = "填写100的倍数", DP1 = "100", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "10", Title = "网咖玩家无限疲劳", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "12", Title = "土罐改版活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "15", Title = "燃烧疲劳活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "16", Title = "完成地下城奖励限定道具", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "19", Title = "金卡活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "24", Title = "组队经验奖励活动", P1Desc = "填写100的倍数", DP1 = "100", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "30", Title = "工会战活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "36", Title = "燃烧金怪活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "49", Title = "创建角色限制", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "50", Title = "强化费用折扣活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "54", Title = "角斗场用点处罚", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "58", Title = "神秘商店活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "87", Title = "周末奖金活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "93", Title = "升级活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "94", Title = "异界地下城活性活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "95", Title = "活动地下城", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "100", Title = "成长型装备活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "101", Title = "消耗疲劳分发道具活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
            EventList.Add(new SimpleEventModel { EventId = "103", Title = "分发成长型道具宠物活动", P1Desc = "填1开启本活动", DP1 = "1", P2Desc = "", DP2 = "0" });
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, null));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        async void DoAddEventCommand()
        {
            var b = await new ActivityService().AddEvent(new Db.Models.ActivityEventModel
            {
                EventType = _eventId,
                Parameter1 = int.Parse(P1),
                Parameter2 = int.Parse(P2),
            });

            Msg = $"活动添加{(b ? "成功" : "失败")},可继续添加";
        }
    }

    struct SimpleEventModel
    {
        public string? EventId { get; set; }
        public string? Title { get; set; }
        public string? P1Desc { get; set; }
        public string? DP1 { get; set; }
        public string? P2Desc { get; set; }
        public string? DP2 { get; set; }
    }
}
