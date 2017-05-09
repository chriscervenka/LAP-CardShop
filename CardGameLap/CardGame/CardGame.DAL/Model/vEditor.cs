namespace CardGame.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vEditor")]
    public partial class vEditor
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idedit { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ideditfile { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idperson { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [StringLength(2000)]
        public string content { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? editdate { get; set; }

        public byte[] editfile { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string firstname { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string lastname { get; set; }
    }
}
