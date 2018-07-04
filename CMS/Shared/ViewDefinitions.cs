using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Shared
{
    public class ViewDefinitions
    {
        public static View FeeCollectionView = new View() { Name = "FeeCollectionView", Title = "Fee Collection" };
        public static View SyncView = new View() { Name = "SyncView", Title = "Sync with Online" };
        public static View SettingsView = new View() { Name = "SettingsView", Title = "Settings" };
        public static View SessionsView = new View() { Name = "SessionsView", Title = "Setup Current Session" };
        public static View CoursesView = new View() { Name = "CoursesView", Title = "Available Courses" };
        public static View GradesView = new View() { Name = "GradesView", Title = "Setup Grades" };
        public static View SectionsView = new View() { Name = "SectionsView", Title = "Setup Sections" };
        public static View UsersView = new View() { Name = "UsersView", Title = "Manage Users" };
        public static View StudentsView = new View() { Name = "StudentsView", Title = "Manage Students" };
        public static View FeeCategoriesView = new View() { Name = "FeeCategoriesView", Title = "Fee Categories" };
        public static View FeeAllocationView = new View() { Name = "FeeAllocationView", Title = "Fee Allocation" };
        public static View DashboardView = new View() { Name = "DashboardView", Title = "Dashboard" };
        public static View FeesStatementReportView = new View() { Name = "FeesStatementReportView", Title = "Fees Statement Report" };
        public static View BalanceFeesReportView = new View() { Name = "BalanceFeesReportView", Title = "Balance Fees Report" };
        public static View TransactionsReportView = new View() { Name = "TransactionsReportView", Title = "Transactions Report" };
    }
}
