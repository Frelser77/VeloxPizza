namespace VeloxPizza.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotto")]
    public partial class Prodotto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotto()
        {
            ProdottoAcquistato = new HashSet<ProdottoAcquistato>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(255)]
        public string NomeProdotto { get; set; }

        [Required]
        [StringLength(255)]
        public string ImgProdotto { get; set; }

        public double PrezzoProdotto { get; set; }

        public int TempoConsegna { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Ingredienti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdottoAcquistato> ProdottoAcquistato { get; set; }
    }
}
