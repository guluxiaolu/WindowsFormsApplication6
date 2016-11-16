using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI; 


namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        AdventureWorksDataSet adventureWorksDS;
        AdventureWorksDataSetTableAdapters.SalesPersonTableAdapter salesPersonTA;
        AdventureWorksDataSetTableAdapters.SalesTerritoryTableAdapter salesTerritoryTA; 

        public Form1()
        {
            InitializeComponent();
            adventureWorksDS = new AdventureWorksDataSet();
            salesPersonTA = new AdventureWorksDataSetTableAdapters.SalesPersonTableAdapter();
            salesTerritoryTA = new AdventureWorksDataSetTableAdapters.SalesTerritoryTableAdapter(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radGridView1.DataSource = adventureWorksDS.SalesTerritory;
            radGridView1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            radGridView1.ThemeName = "VistaOrange";
            radGridView1.MasterTemplate.AllowAddNewRow = false;
            radGridView1.Columns["rowguid"].IsVisible = false;

            GridViewTemplate childTmpt = new GridViewTemplate();
            childTmpt.DataSource = adventureWorksDS.SalesPerson;
            childTmpt.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            childTmpt.AllowAddNewRow = false;
            radGridView1.MasterTemplate.Templates.Add(childTmpt);

            GridViewRelation relation = new GridViewRelation(radGridView1.MasterTemplate);
            relation.ChildTemplate = childTmpt;
            relation.RelationName = "SalesTerritoryPerson";
            relation.ParentColumnNames.Add("TerritoryID");
            relation.ChildColumnNames.Add("TerritoryID");
            radGridView1.Relations.Add(relation);

            salesTerritoryTA.Fill(adventureWorksDS.SalesTerritory);
            salesPersonTA.Fill(adventureWorksDS.SalesPerson);
            childTmpt.Columns["rowguid"].IsVisible = false; 
        }
    }
}
