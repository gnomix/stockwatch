using System.Windows;
using gorilla.infrastructure.container;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views;
using solidware.financials.windows.ui.views.icons;
using utility;

namespace solidware.financials.windows.ui.bootstrappers
{
    public class ComposeShell : NeedStartup
    {
        RegionManager region_manager;
        ApplicationController controller;
        DialogLauncher launcher;

        public ComposeShell(RegionManager region_manager, ApplicationController controller, DialogLauncher launcher)
        {
            this.region_manager = region_manager;
            this.launcher = launcher;
            this.controller = controller;
        }

        public void run()
        {
            controller.add_tab<TaxSummaryPresenter, TaxSummaryTab>();

            region_manager.region<MainMenu>(x =>
            {
                x.add("_Application").add("E_xit", () => Resolve.the<Shell>().Close());
                x.add("_Family").add("_Add Member", launch<AddFamilyMemberPresenter, AddFamilyMemberDialog>).apply_icon(UIIcon.Plus).parent();
                x.add("_Income").add("_Add Income", launch<AddNewIncomeViewModel, AddNewIncomeDialog>).apply_icon(UIIcon.Plus);
                //x.add("_Deductions").add("_Add RRSP", () => { }) ;
                //x.add("_Credits").add("_Add Credit", () => { }) ;
                //x.add("_Benefits").add("_Add Benefit", () => { }) ;
                //x.add("_Window").add("_Taxes", () => controller.add_tab<TaxSummaryPresenter, TaxSummaryTab>()).apply_icon(UIIcon.Category);
                x.add("_Help").add("_Taxes", launch<DisplayCanadianTaxInformationViewModel, DisplayCanadianTaxInformationDialog>).apply_icon(UIIcon.Help);
            });

            controller.load_region<StatusBarPresenter, StatusBarRegion>();
            controller.load_region<ButtonBarPresenter, ButtonBar>();
            region_manager.region<ButtonBar>(x =>
            {
                x.AddCommand("Add Family Member", launch<AddFamilyMemberPresenter, AddFamilyMemberDialog>, UIIcon.Plus);
                x.AddCommand("Add Income", launch<AddNewIncomeViewModel, AddNewIncomeDialog>, UIIcon.Plus);
            });
        }

        void launch<Presenter, Dialog>() where Presenter : DialogPresenter
            where Dialog : FrameworkElement, Dialog<Presenter>, new()
        {
            launcher.launch<Presenter, Dialog>();
        }
    }
}