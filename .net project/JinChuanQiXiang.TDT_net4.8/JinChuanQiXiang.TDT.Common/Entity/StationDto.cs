using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace JinChuanQiXiang.TDT.Common.Entity
{
    public class StationDto
    {
        /// <summary>
        /// 站名
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 站号
        /// </summary>
        public string StationNo { get; set; }
        /// <summary>
        /// 所在乡镇
        /// </summary>
        public string StationTown { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// 测站级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 是否观测气压
        /// </summary>
        public string HasQiYa { get; set; }
        /// <summary>
        /// 是否观测气温
        /// </summary>
        public string HasQiWen { get; set; }
        /// <summary>
        /// 是否观测风
        /// </summary>
        public string HasFeng { get; set; }
        /// <summary>
        /// 是否观测降水
        /// </summary>
        public string HasJiangShui { get; set; }
        /// <summary>
        /// 是否观测相对湿度
        /// </summary>
        public string HasXiangDuiShiDu { get; set; }
        /// <summary>
        /// 是否观测能见度
        /// </summary>
        public string HasNengJianDu { get; set; }

        //Parse方法会在自定义读写CSV文件时用到
        //public static StationDto Parse(string[] fields)
        //{
        //    try
        //    {
        //        StationDto station = new StationDto();
        //        station.StationName = fields[1];
        //        station.StationNo = fields[2];
        //        station.StationTown = fields[3];
        //        station.Longitude = fields[4];
        //        station.Latitude = fields[5];
        //        station.Level = fields[6];
        //        station.HasQiYa = fields[7]?.Trim()=="是";
        //        station.HasQiWen = fields[8]?.Trim() == "是";
        //        station.HasFeng = fields[9]?.Trim() == "是";
        //        station.HasJiangShui = fields[10]?.Trim() == "是";
        //        station.HasXiangDuiShiDu = fields[11]?.Trim() == "是";
        //        station.HasNengJianDu = fields[12]?.Trim() == "是";
        //        return station;
        //    }
        //    catch (Exception)
        //    {
        //        //做一些异常处理，写日志之类的
        //        return null;
        //    }
        //}
    }
    public sealed class StationDtoMap : ClassMap<StationDto>
    {
        StationDtoMap()
        {
            //使用文件列名称指定映射
            Map(m => m.StationName).Name("站名");  
            Map(m => m.StationNo).Name("站号");
            Map(m => m.StationTown).Name("乡镇");
            Map(m => m.Longitude).Name("经度");

            Map(m => m.Latitude).Name("纬度");
            Map(m => m.Level).Name("测站级别");
            Map(m => m.HasQiYa).Name("气压");
            Map(m => m.HasQiWen).Name("气温");

            Map(m => m.HasFeng).Name("风");
            Map(m => m.HasJiangShui).Name("降水");
            Map(m => m.HasXiangDuiShiDu).Name("相对湿度");
            Map(m => m.HasNengJianDu).Name("能见度");
        }
    }
}
