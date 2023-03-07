using AutoMapper.Configuration.Annotations;

namespace Energy.Models.ViewModels.Database
{
    /// <summary>
    /// 下拉選單
    /// </summary>
    public class DropItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// 子項目名稱
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 深度(0時, 代表第一層)
        /// </summary>
        [Ignore]
        public int? Depth { get; set; }

        /// <summary>
        /// 父節點Id
        /// </summary>
        [Ignore]
        public string ParentId { get; set; } = null!;

        /// <summary>
        /// 項目名稱
        /// </summary>
        public string PageName { get; set; } = null!;

        /// <summary>
        /// 是否顯示CheckBox
        /// </summary>
        public bool ShowCheckBox { get; set; }

        /// <summary>
        /// 順序
        /// </summary>
        [Ignore]
        public int Iorder { get; set; }

        /// <summary>
        /// 子項目
        /// </summary>
        public List<DropItem> Children { get; set; } = null!;
    }
}
