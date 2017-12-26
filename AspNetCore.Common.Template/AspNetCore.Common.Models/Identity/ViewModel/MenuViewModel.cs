using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Models.Identity.ViewModel
{
    [ProtoContract]
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            ClildMenu = new List<MenuViewModel>();
        }
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public int? ParentId { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [ProtoMember(3)]
        public string Icon { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        [ProtoMember(4)]
        public string Name { get; set; }

        /// <summary>
        /// 地址URL
        /// </summary>
        [ProtoMember(5)]
        public string Value { get; set; }

        [ProtoMember(6)]
        public List<MenuViewModel> ClildMenu { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [ProtoMember(7)]
        public int Sequence { get; set; }
    }
}
