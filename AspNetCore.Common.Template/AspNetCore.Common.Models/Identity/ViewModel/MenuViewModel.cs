using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Models.Identity.ViewModel
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            ClildMenu = new List<MenuViewModel>();
        }
        public int Id { get; set; }

        public int? ParentId { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址URL
        /// </summary>
        public string Value { get; set; }

        public List<MenuViewModel> ClildMenu { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sequence { get; set; }
    }
}
