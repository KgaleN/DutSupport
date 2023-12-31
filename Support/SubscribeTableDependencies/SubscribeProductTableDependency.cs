﻿using Microsoft.AspNet.SignalR;
using Microsoft.Data.SqlClient;
using Support.Hubs;
using Support.Models;
using TableDependency.SqlClient;

namespace Support.SubscribeTableDependencies
{
    public class SubscribeProductTableDependency: ISubscribeTableDependency
    {
        SqlTableDependency<Ticket> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeProductTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Ticket>(connectionString,"dbo.Tickets");
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Ticket> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
              await dashboardHub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Ticket)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
