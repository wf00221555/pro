using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataGridView1.RowHeadersVisible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int zgfcount = 0;
            int pfcount = 0;
            int wgfcount = 0;
            int zgfwb = 0;
            int pfwb = 0;
            int wgfwb = 0;

            //DateTime zgfstime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 7:00");
            //DateTime zgfetime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 9:00");
            //DateTime pfstime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 9:00");
            //DateTime pfetime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 16:30");
            //DateTime wgfstime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 16:30");
            //DateTime wgfetime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 18:30");
            try
            {
                DataTable dt = GetDataFromExcelByConn();
                DataTable newdt = new DataTable();
                DataColumn dc1 = new DataColumn("日期", typeof(string));
                DataColumn dc2 = new DataColumn("中队", typeof(string));
                DataColumn dc3 = new DataColumn("道路名称", typeof(string));
                DataColumn dc4 = new DataColumn("拥堵起点", typeof(string));
                DataColumn dc5 = new DataColumn("拥堵终点", typeof(string));
                DataColumn dc6 = new DataColumn("开始时间", typeof(string));
                DataColumn dc7 = new DataColumn("结束时间", typeof(string));
                newdt.Columns.Add(dc1);
                newdt.Columns.Add(dc2);
                newdt.Columns.Add(dc3);
                newdt.Columns.Add(dc4);
                newdt.Columns.Add(dc5);
                newdt.Columns.Add(dc6);
                newdt.Columns.Add(dc7);

                if (dt != null)
                {
                    for (int i = dt.Rows.Count - 1; i > 0; i--)
                    {
                        TimeSpan starttime = Convert.ToDateTime(dt.Rows[i][1].ToString().Replace("\"", "")).TimeOfDay;
                        TimeSpan endtime = Convert.ToDateTime(dt.Rows[i][8].ToString().Replace("\"", "")).TimeOfDay;

                        //统计
                        if (starttime >= TimeSpan.Parse("7:00") && endtime <= TimeSpan.Parse("9:00"))
                        {
                            zgfcount++;
                            if (dt.Rows[i][0].ToString().Contains("半山互通") || dt.Rows[i][0].ToString().Contains("高架"))
                            {
                                zgfwb++;
                                DataRow dr = newdt.NewRow();
                                dr["日期"] = DateTime.Now.ToShortDateString();
                                dr["中队"] = "半山";
                                dr["道路名称"] = dt.Rows[i][0].ToString().Replace("\"", "");
                                dr["拥堵起点"] = dt.Rows[i][2].ToString().Replace("\"", "");
                                dr["拥堵终点"] = dt.Rows[i][3].ToString().Replace("\"", "");
                                dr["开始时间"] = dt.Rows[i][1].ToString().Replace("\"", "");
                                dr["结束时间"] = dt.Rows[i][8].ToString().Replace("\"", "");
                                newdt.Rows.Add(dr);
                            }
                        }
                        else if (starttime > TimeSpan.Parse("9:00") && endtime < TimeSpan.Parse("16:30"))
                        {
                            pfcount++;
                            if (dt.Rows[i][0].ToString().Contains("半山互通") || dt.Rows[i][0].ToString().Contains("高架"))
                            {
                                pfwb++;
                                DataRow dr = newdt.NewRow();
                                dr["日期"] = DateTime.Now.ToShortDateString();
                                dr["中队"] = "半山";
                                dr["道路名称"] = dt.Rows[i][0].ToString().Replace("\"", "");
                                dr["拥堵起点"] = dt.Rows[i][2].ToString().Replace("\"", "");
                                dr["拥堵终点"] = dt.Rows[i][3].ToString().Replace("\"", "");
                                dr["开始时间"] = dt.Rows[i][1].ToString().Replace("\"", "");
                                dr["结束时间"] = dt.Rows[i][8].ToString().Replace("\"", "");
                                newdt.Rows.Add(dr);
                            }
                        }
                        else if (starttime >= TimeSpan.Parse("16:30") && endtime <= TimeSpan.Parse("18:30"))
                        {
                            wgfcount++;
                            if (dt.Rows[i][0].ToString().Contains("半山互通") || dt.Rows[i][0].ToString().Contains("高架"))
                            {
                                wgfwb++;
                                DataRow dr = newdt.NewRow();
                                dr["日期"] = DateTime.Now.ToShortDateString();
                                dr["中队"] = "半山";
                                dr["道路名称"] = dt.Rows[i][0].ToString().Replace("\"", "");
                                dr["拥堵起点"] = dt.Rows[i][2].ToString().Replace("\"", "");
                                dr["拥堵终点"] = dt.Rows[i][3].ToString().Replace("\"", "");
                                dr["开始时间"] = dt.Rows[i][1].ToString().Replace("\"", "");
                                dr["结束时间"] = dt.Rows[i][8].ToString().Replace("\"", "");
                                newdt.Rows.Add(dr);
                            }
                        }
                        else if (dt.Rows[i][0].ToString().Contains("半山互通") || dt.Rows[i][0].ToString().Contains("高架"))
                        {
                            if (dt.Rows[i][0].ToString().Contains("半山互通") || dt.Rows[i][0].ToString().Contains("高架"))
                            {
                                DataRow dr = newdt.NewRow();
                                dr["日期"] = DateTime.Now.ToShortDateString();
                                dr["中队"] = "半山";
                                dr["道路名称"] = dt.Rows[i][0].ToString().Replace("\"", "");
                                dr["拥堵起点"] = dt.Rows[i][2].ToString().Replace("\"", "");
                                dr["拥堵终点"] = dt.Rows[i][3].ToString().Replace("\"", "");
                                dr["开始时间"] = dt.Rows[i][1].ToString().Replace("\"", "");
                                dr["结束时间"] = dt.Rows[i][8].ToString().Replace("\"", "");
                                newdt.Rows.Add(dr);
                            }
                        }
                    }


                    dataGridView1.DataSource = newdt;
                    label3.Text = zgfcount.ToString();
                    label4.Text = zgfwb.ToString();
                    label7.Text = pfcount.ToString();
                    label8.Text = pfwb.ToString();
                    label11.Text = wgfcount.ToString();
                    label12.Text = wgfwb.ToString();
                    label15.Text = (zgfcount + pfcount + wgfcount).ToString();
                    label16.Text = (zgfwb + pfwb + wgfwb).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private DataTable GetDataFromExcelByConn()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel(*.csv)|*.csv|Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.Cancel) return null;
            var filePath = openFile.FileName;

            if (Path.GetExtension(filePath) == ".csv")
            {
                DataTable mycsvdt = new DataTable();
                int intColCount = 0;
                bool blnFlag = true;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                StreamReader mysr = new StreamReader(filePath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });

                    //给datatable加上列名
                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryline.Length;
                        int col = 0;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            col = i + 1;
                            mydc = new DataColumn(col.ToString());
                            mycsvdt.Columns.Add(mydc);
                        }
                    }

                    //填充数据并加入到datatable中
                    mydr = mycsvdt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        mydr[i] = aryline[i];
                    }
                    mycsvdt.Rows.Add(mydr);
                }
                return mycsvdt;
            }
            else
            {
                string fileType = System.IO.Path.GetExtension(filePath);
                if (string.IsNullOrEmpty(fileType)) return null;

                using (DataSet ds = new DataSet())
                {
                    string strCon = string.Format("Provider=Microsoft.ACE.OLEDB.{0}.0;" +
                                    "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";" +
                                    "data source={3};",
                                    (fileType == ".xls" ? 4 : 12), (fileType == ".xls" ? 8 : 12), (true ? "Yes" : "NO"), filePath);

                    string strCom = " SELECT * FROM [" + Path.GetFileNameWithoutExtension(filePath) + "]";
                    using (OleDbConnection myConn = new OleDbConnection(strCon))
                    using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
                    {
                        myConn.Open();
                        myCommand.Fill(ds);
                    }
                    if (ds == null || ds.Tables.Count <= 0) return null;
                    return ds.Tables[0];
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

