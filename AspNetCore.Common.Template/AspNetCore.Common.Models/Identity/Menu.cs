using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCore.Common.Models.Identity
{
    public class Menu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        public int? ParentId { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址URL
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 是否为叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }
    }
}
