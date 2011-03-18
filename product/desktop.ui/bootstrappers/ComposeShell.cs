using System.Windows;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views;

namespace solidware.financials.windows.ui.bootstrappers
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
            controller.add_tab<TaxSummaryPresenter, TaxSummaryTab>();

            region_manager.region<MainMenu>(x =>
            {
                x.add("_Family")
                    .add("_Add Member", launch<AddFamilyMemberPresenter, AddFamilyMemberDialog>);
                x.add("_Income")
                    .add("_Add Income", launch<AddNewIncomeViewModel, AddNewIncomeDialog>);
                //x.add("_Deductions").add("_Add RRSP", () => { }) ;
                //x.add("_Credits").add("_Add Credit", () => { }) ;
                //x.add("_Benefits").add("_Add Benefit", () => { }) ;
                x.add("_Help")
                    .add("_Taxes", launch<DisplayCanadianTaxInformationViewModel, DisplayCanadianTaxInformationDialog>);
            });

            controller.load_region<StatusBarPresenter, StatusBarRegion>();
            controller.load_region<SelectedFamilyMemberPresenter, SelectedFamilyMemberRegion>();
        }

        void launch<Presenter, Dialog>() where Presenter : DialogPresenter
            where Dialog : FrameworkElement, Dialog<Presenter>, new()
        {
            controller.launch<Presenter, Dialog>();
        }
    }
}