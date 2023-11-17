using Rocket.API;

namespace SK_JoinLeave
{
    public class SK_JoinLeaveConfig : IRocketPluginConfiguration
    {
        public bool JoinMessageEnable = true;
        public bool LeaveMessageEnable = true;
        public string JoinMessageColor { get; set; }
        public string LeaveMessageColor { get; set; }
        public void LoadDefaults()
        {
            JoinMessageColor = "green";
            LeaveMessageColor = "red";
        }
    }
}