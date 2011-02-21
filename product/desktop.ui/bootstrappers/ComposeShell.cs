using desktop.ui.presenters;
using desktop.ui.views;

namespace desktop.ui.bootstrappers
{
    public class ComposeShell : NeedStartup
    {
        RegionManager region_manager;
        ApplicationController controller;

        public ComposeShell(RegionManager region_manager, ApplicationController controller)
        {
            this.region_manager = region_manager;
            this.controller = controller;
        }

        public void run()
        {
            controller.add_tab<AccountPresenter, AccountTab>();

            region_manager.region<MainMenu>(x =>
            {
                x.add("_Family").add("_Add Member", () =>
                {
                    controller.launch_dialog<AddFamilyMemberPresenter, AddFamilyMemberDialog>();
                });
                x.add("_Accounts").add("_Add Account", () => { 
                    controller.launch_dialog<AddNewDetailAccountPresenter, AddNewDetailAccountDialog>();
                });
            });

            controller.load_region<StatusBarPresenter, StatusBarRegion>();
            controller.load_region<SelectedFamilyMemberPresenter, SelectedFamilyMemberRegion>();
        }
    }
}