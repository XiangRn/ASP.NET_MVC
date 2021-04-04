namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [StringLength(10, ErrorMessage = "Số ký tự tối đa là 10")]
        [DisplayName("Tên danh mục")]
        [Required(ErrorMessage = "Bạn chưa nhập tên danh mục")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Tiêu đề SEO")]
        public string Alias { get; set; }

        [DisplayName("Danh mục cha")]
        public int? ParentID { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Thứ tự")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public int? Order { get; set; }

        [DisplayName("Trạng Thái")]
        public bool? Status { get; set; }
        public int ID { get; set; }
    }
}
