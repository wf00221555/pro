
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    //如果好用，请收藏地址，帮忙分享。
    public class Disposal
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string congestionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int adCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cause { get; set; }
        /// <summary>
        /// 由于发生车流量大造成拥堵，可以通行，交警已抵达现场。; 已派警，警察已到现场
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int estimatedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string congestImg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int disposeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string disposeTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string disposeUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string disposeUsername { get; set; }
        /// <summary>
        /// 拱墅半山中队
        /// </summary>
        public string orgName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtModified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int orgId { get; set; }
        /// <summary>
        /// 半山路
        /// </summary>
        public string roadName { get; set; }
    }

    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string congestId { get; set; }
        /// <summary>
        /// 半山路
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 拱墅区半山街道半山路老年病医院
        /// </summary>
        public string initStartAddr { get; set; }
        /// <summary>
        /// 拱墅区半山街道半山路
        /// </summary>
        public string endAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string initStartPoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string endPoint { get; set; }
        /// <summary>
        /// 北向南
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int speed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int congestLength { get; set; }
        /// <summary>
        /// 国道
        /// </summary>
        public string roadType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string occurTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int continuedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string x { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string supervise { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isTransiency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isRealtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string handle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Disposal disposal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string causeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string superviseSent { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public int totalNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int filterNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }

}